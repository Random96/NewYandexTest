using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmlSoft.KBSTest.Domain
{
    public class Validator
    {
        const string Pattern = @"^(https?:\/\/)?" + // protocol
           @"((([a-z\d]([a-z\d-]*[a-z\d])*)\.)+[a-z]{2,}|" + // domain name
           @"((\d{1,3}\.){3}\d{1,3}))" + // OR ip (v4) address
           @"(\:\d+)?(\/[-a-z\d%_.~+]*)*" + // port and path
           @"(\?[;&a-z\d%_.~+=-]*)?" + // query string
           @"(\#[-a-z\d_]*)?$"; // fragment locater

        public static ValidationResult ValidateUrl(string Url)
        {
            throw new Exception("qq");
            if (Regex.IsMatch(Url, Pattern))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Введите правильный URL");
            }
        }
    }
}
