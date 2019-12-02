using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace KalkuladoraWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            return View();
        }

        [HttpPost]
        public string Calc(string input)
        {
            string pattern = @"(\+)|(\−)|(\÷)|(\×)"; // is a math symbol
            
            string[] expression = Regex.Split(input, pattern, RegexOptions.IgnoreCase,
                TimeSpan.FromMilliseconds(500));
            Console.WriteLine(string.Join("", expression));
            //multiplicações e divisões primeiro
            for (int i = 0; i < expression.Length; i++)
            {
                bool isSymbol = Regex.IsMatch(expression[i], pattern);
                if (isSymbol)
                {
                    float result = 0;
                    if (expression[i] == "×")
                    {
                        result = float.Parse(expression[i - 1]) * float.Parse(expression[i + 1]);
                    }
                    else if (expression[i] == "÷")
                    {
                        result = float.Parse(expression[i - 1]) / float.Parse(expression[i + 1]);
                    }
                    else
                    {
                        continue;
                    }
                    expression[i - 1] = result.ToString();
                    for (int j = i + 2; j < expression.Length; j++)
                    {
                        expression[j - 2] = expression[j];
                    }
                    expression[expression.Length - 1] = null;
                    expression[expression.Length - 2] = null;
                    expression = expression.Where(w => w != null).ToArray();
                    i -= 2;
                    Console.WriteLine(string.Join("", expression));
                }
            }

            // adições e subtrações
            for (int i = 0; i < expression.Length; i++)
            {
                bool isSymbol = Regex.IsMatch(expression[i], pattern);
                if (isSymbol)
                {
                    float result = 0;
                    if (expression[i] == "+")
                    {
                        result = float.Parse(expression[i - 1]) + float.Parse(expression[i + 1]);
                    }
                    else if (expression[i] == "−")
                    {
                        result = float.Parse(expression[i - 1]) - float.Parse(expression[i + 1]);
                    }
                    else
                    {
                        continue;
                    }
                    expression[i - 1] = result.ToString();
                    for (int j = i + 2; j < expression.Length; j++)
                    {
                        expression[j - 2] = expression[j];
                    }
                    expression[expression.Length - 1] = null;
                    expression[expression.Length - 2] = null;
                    expression = expression.Where(w => w != null).ToArray();
                    i -= 2;
                    Console.WriteLine(string.Join("", expression));
                }
            }

            Console.WriteLine(string.Join("", expression));

            return string.Join("", expression);
        }
        
    }
}
