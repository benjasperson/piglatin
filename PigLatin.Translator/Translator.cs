/********************************************************************
 * 
 * This class defines the methods needed for the PigLat translator
 * 
 * ******************************************************************/
using System.Text.RegularExpressions;


namespace PigLatin.Translator
{
    public class Translator
    {
        //All methods are static so no constructor is needed

        private static char[] punctuation = { '!', '?', ',', '.', ':', ';' }; //list of acceptable punctuation

        //public method used to translate each line of text
        public static string convertToLatin(string line)
        {
            string[] words = line.Split(' ');
            string result = "";
            for (int i = 0; i < words.Length; i++)
            {
                result += translateWord(words[i]) +
                    (words.Length - 1 == i ? "" : " ");
            }
            return result;
        }

        //used by the convertToLatin method to translate a single word from a line of text
        private static string translateWord(string word)
        {
            //check that the word is translatable
            if (isTranslatable(word))
            {
                string wordWithoutPunc = word.Split(punctuation)[0];
                string punc = word.Substring(wordWithoutPunc.Length);

                string firstLetter = wordWithoutPunc.Substring(0, 1);

                //Add a 'w' if the word starts with a vowel
                if (isVowel(firstLetter.ToCharArray()[0]))
                {
                    return wordWithoutPunc + "way" + punc;
                }
                else
                {
                    //Check length
                    if (wordWithoutPunc.Length < 2)
                    {
                        return wordWithoutPunc + "ay" + punc;
                    }
                    else
                    {
                        string latinWord =
                            wordWithoutPunc.Substring(1, wordWithoutPunc.Length - 1) +
                            firstLetter + "ay" + punc;

                        //Check for capitalization
                        if (firstLetter.ToUpper() == firstLetter)
                        {
                            latinWord = fixCapitalization(wordWithoutPunc) + "ay" + punc;
                        }

                        return latinWord;
                    }
                }
            }
            else
            {
                //If the word contians unexpected characters or is otherwise untranslatable,
                // return the word as is
                return word;
            }
        }

        
        private static string fixCapitalization(string word)
        {

            //Check that word has multiple letters
            if (word.Length > 1 )
            {
                return word.Substring(1, 1).ToUpper() +  //second letter made upper case
                    (word.Length > 2 ? word.Substring(2,word.Length - 2) : "") + //beyond second if available
                    word.Substring(0, 1).ToLower(); //first letter made lower case
            }
             
            return null;
        }

        //Usesd to verify that the starts with letters and ends with punctation.
        //The word also needs to have at least one character
        private static bool isTranslatable(string word)
        {
             return new Regex("^[A-z]*[[!?.,:;]*$").IsMatch(word) && word.Length > 0;

        }

        //Used to see if a character is a vowel
        private static bool isVowel(char letter)
        {
            return new Regex("[A,a,E,e,I,i,O,o,U,u]+").IsMatch(letter.ToString());
        }
    }
}
