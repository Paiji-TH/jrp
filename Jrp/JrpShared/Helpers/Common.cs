using System.Linq;

namespace JrpShared.Helpers
{
    public static class Common
    {
        public static bool ValidateCharacterName(ref string name)
        {
            string[] credentials = name.Split();

            if (name.Split().Length == 2)
            {
                if (credentials[0].All(char.IsLetter) && credentials[1].All(char.IsLetter))
                {
                    if (credentials[0].Length > 2 && credentials[0].Length < 26 && credentials[1].Length > 2 && credentials[1].Length < 26)
                    {
                        credentials[0] = credentials[0].ToLower();
                        credentials[1] = credentials[1].ToLower();
                        credentials[0] = credentials[0][0].ToString().ToUpper() + credentials[0].Substring(1);
                        credentials[1] = credentials[1][0].ToString().ToUpper() + credentials[1].Substring(1);

                        name = credentials[0] + " " + credentials[1];

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
