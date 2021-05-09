using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace ExpleoTestApp
{
   public static class Calculator
    {       
        //Metoden hanterar eventuella felaktiga inmatningar, och klarar av alla tre nivåer
        public static (double,string) EvaluateExpressionEveryLevel(string expression)
        {
            string tidyExpression = Regex.Replace(expression, @"[^0-9+/\-*.,]", string.Empty).Replace(",",".");
            //Använding av Regex för att ersätta oönskade chars med en tomsträng, 
            //Jag ersätter allt som inte är 0-9, och de fyra uttrycks operatorerna "+,-,/,*"
            //För att vissa vill skriva 5.5 som 5,5 exempelvis så ersätter jag avslutningsvis "," med . för att göra decimaluträkningar

            return (double.Parse(new DataTable().Compute(tidyExpression,null).ToString()),tidyExpression);
            //Mycket magi som händer här, Börjar med att konvertera strängen till en double, 
            //Datatable returnerar ett objekt så den behöver sättas till en sträng för att kunna konverteras 
            //skapar en tilfällig tabell, som räknar ut det städade uttrycket

        }

        //Tänkt till att vara återanvändbar, men fungerade ej för Level3
        private static (List<double>,List<char>) ListFiller(string tidyExpression)
        {
            List<double> numbers = new();
            List<char> operatorChars = new();

            //Dessa två variabler håller koll på operations position och count lagrar hur många siffror jag loopat över
            //används för att lyfta ut siffrorna mha substring
            int operatorPos = 0;
            int count = 0;
            for (int i = 0; i < tidyExpression.Length; i++)
            {
                if (Char.IsDigit(tidyExpression[i]))
                    count++;
                if (!Char.IsDigit(tidyExpression[i]))
                {
                    operatorChars.Add(tidyExpression[i]);
                    //Lyfter ut en substräng baserat på operatorns positon, - hur många siffor som loopats över, 
                    //fram till hur många siffror som loopats över, dvs för att få ut "2" på index 0: + ligger på index[1], så 1-1 = 0
                    //startpositionen för var vi lyfter, slutpositonen är count vilket är 1 eftersom vi loopat över 1 siffra.
                    //Så den plockar en substring som börjar på 0 och slutar på 1 vilket är index[0] som är "2";
                    numbers.Add(double.Parse(tidyExpression.Substring(i - count, count)));
                    count = 0;
                    operatorPos = i;
                }
            }
            numbers.Add(double.Parse(new string(tidyExpression.Substring(operatorPos + 1)))); //Lägger till den sista siffran mha operatorpos
            return (numbers, operatorChars);
        }
        //Metoder utan den givna magin från DataTable.Compute
        public static (double,string) EvaluteExpressionLevelOne(string expression)
        {   //Städar bort oönskade tecken
            string tidyExpression = Regex.Replace(expression, @"[^0-9+/\-*.,]", string.Empty).Replace(",", ".");
            
            //Variabler som ska hålla koll på siffrorna och operatortecknet.
            string rightNum = ""; 
            string leftNum = "";
            string exOperator = "";


            //Går igenom varje tecken i den städaden strängen.
            for (int i = 0; i < tidyExpression.Length; i++)
            {
                //Om ett tecken inte är en siffra, så börjas tilldelningen av värden.
                if (!Char.IsDigit(tidyExpression[i]))
                {
                    exOperator += tidyExpression[i]; //operatorn
                    rightNum += tidyExpression.Substring(i+1); //lyfter ut den del av strängen som börjar efter operatortecknet
                    leftNum += tidyExpression.Remove(i); // tar bort allting från/inkluderat operatortecknet till slutet av strängen.
                }
            }

            //en enkel switch med parse för att sköta uträkningen
            switch (exOperator)
            {
                case "+":
                    return (double.Parse(leftNum) + double.Parse(rightNum),tidyExpression);
                case "-":
                    return (double.Parse(leftNum) - double.Parse(rightNum),tidyExpression);
                case "*":
                    return (double.Parse(leftNum) * double.Parse(rightNum),tidyExpression);
                default:
                    return (double.Parse(leftNum) / double.Parse(rightNum),tidyExpression);
            }          
        }

        public static (double,string) EvaluteExpressionLevelTwo(string expression)
        {   //samma som tidigare dålig DRY
            string tidyExpression = Regex.Replace(expression, @"[^0-9+/\-*.,]", string.Empty).Replace(",", ".");

            //De två listorna som kommer lagra siffrorna och operatorerna
            List<double> numbers;         
            List<char> operatorChars;

            (numbers, operatorChars) = ListFiller(tidyExpression); // Fyller listerna från ListFiller metoden

            double sum = numbers[0]; // Summan sätts till startindex och kommer inkrementeras i switchen

            //går igenom varje operatortecken
            for (int i = 0; i < operatorChars.Count; i++)
            {
                //Switchen sköter återigen uträkningen
                switch (operatorChars[i])
                {
                    case '+':
                        sum += numbers[i + 1]; //Summan är satt till index 0 så sum adderas till nästa index.           
                        break;
                    case '-':
                         sum -= numbers[i + 1];
                        break;
                    case '*':
                        sum *= numbers[i + 1];
                        break;
                    default:
                        if (numbers[i + 1] == 0 || sum == 0)
                            return (0,tidyExpression);
                        else
                        sum /= numbers[i + 1];
                        break;
                }
            }
            return (sum,tidyExpression);
        }

        public static (double,string) EvaluteExpressionLevelThree(string expression)
        {
            string tidyExpression = Regex.Replace(expression, @"[^0-9+/\-*.,]", string.Empty).Replace(",", ".");
            List<double> numbers;
            List<char> operatorChars;

            (numbers, operatorChars) = ListFiller(tidyExpression); //Samma som ovan

            double firstRemovedNum; //Variabler för att hålla värdena som lyfts ur listan
            double secondRemovedNum;  
            for (int i = 0; i < operatorChars.Count; i++) 
            {
                if (operatorChars[i] == '*' || operatorChars[i] == '/') //För att kunna räkna rätt enligt mattematiska principer måste * och / räknas ut först
                {                                                   
                    char removedChar = operatorChars[i];
                    operatorChars.RemoveAt(i); 
                    //lagrar operatorerna och tilldelar värden, tar sedan bor dem ur listan av siffror
                    firstRemovedNum = numbers[i];
                    secondRemovedNum = numbers[i + 1]; 
                    numbers.RemoveAt(i); 
                    numbers.RemoveAt(i);

                    if (removedChar == '*')
                        numbers.Insert(i, firstRemovedNum * secondRemovedNum); //sätter tillbaka den uträknade summan av numrena som lyftes ut.
                    if (removedChar == '/')
                    {
                        if (firstRemovedNum == 0 || secondRemovedNum == 0) // Tar hand om delning med 0
                            return (0,tidyExpression);
                        else
                            numbers.Insert(i, firstRemovedNum / secondRemovedNum);
                    }
                    i--;
                }
            }

            double res = numbers[0];
            //switch sköter beräkningen av kvarvarande operatorer
            for (int i = 0; i < operatorChars.Count; i++)
            {
                if(operatorChars[i] == '+')
                {
                    res += numbers[i + 1];    
                }
                if(operatorChars[i] == '-')
                {
                    res -= numbers[i + 1];
                }               
            }
            return (res,tidyExpression);
        }
    }
}
