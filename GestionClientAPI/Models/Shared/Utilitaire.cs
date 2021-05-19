using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;

namespace GestionClientAPI.Models.Shared
{
    public class Utilitaire
    {
        // hash un mot de passe ; from https://www.youtube.com/watch?v=N4tTYn43boo
        public static string HashPassword(string password)
        {
            using var sha = SHA256.Create();

            var passwordBytes = Encoding.Default.GetBytes(password);

            var hashedPassword = sha.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hashedPassword);
        }

        public static bool VerifyPassword(string password, string passwordToTest)
        {
            return (password.Equals(HashPassword(passwordToTest)));
        }


        // Transforme une énumération en Observable ; le madlad : https://stackoverflow.com/questions/9984594/iqueryablea-to-observablecollectiona-where-a-anonymous-type
        public static ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }
    }
}
