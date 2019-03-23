using System;
using System.Text.RegularExpressions;


namespace KMA.ProgrammingInCSharp2019.lab4.Models
{
    [Serializable]
    public class User
    {
        #region Fields

        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDateTime;

        private readonly string[] _westSigns = { "Capricorn", "Aquarius", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Sagittarius" };
        private readonly string[] _chineseSigns = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };


        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
             set
            {
                Regex regex = new Regex(@"^[A-zА-яЄєЇїІі][A-zА-яЄєЇїІі|\.|\s|-]+$");
                Match match = regex.Match(value);
                if (!match.Success)
                {
                    throw new InvalidNameException();
                }
                _name = value;
            }
        }

        public string Surname
        {
            get { return _surname; }
             set
            {
                Regex regex = new Regex(@"^[A-zА-яЄєЇїІі][A-zА-яЄєЇїІі|-]+$");
                Match match = regex.Match(value);
                if (!match.Success)
                {
                    throw new InvalidNameException();
                }
                _surname = value;
            }
        }

        public string Email
        {
            get { return _email;}
             set
            {
                var addr = new System.Net.Mail.MailAddress(value);
                if (addr.Address != value)
                {
                    throw new EmailException();     
                }
                _email = value;
                
               
            }
        }

        public DateTime BirthDate
        {
            get { return _birthDateTime; }
             set
            {
                if (value < DateTime.Today.AddYears(-135))
                {
                    throw new OldDateException();
                }
                if(    value > DateTime.Now) { 
               
                    throw new FutureDateException();
                }
                 _birthDateTime = value;
                

            }
        }

        public string SunSign
        {
            get
            {
                switch (BirthDate.Month)
                {
                    case 1:
                       return BirthDate.Day >= 20 ? _westSigns[1] : _westSigns[0];
                    case 2:
                        return BirthDate.Day >= 19 ? _westSigns[2] : _westSigns[1];
                    case 3:
                        return BirthDate.Day >= 21 ? _westSigns[3] : _westSigns[2];
                    case 4:
                        return BirthDate.Day >= 20 ? _westSigns[4] : _westSigns[3];
                    case 5:
                        return BirthDate.Day >= 21 ? _westSigns[5] : _westSigns[4];
                    case 6:
                        return BirthDate.Day >= 21 ? _westSigns[6] : _westSigns[5];
                    case 7:
                        return BirthDate.Day >= 23 ? _westSigns[7] : _westSigns[6];
                    case 8:
                        return BirthDate.Day >= 23 ? _westSigns[8] : _westSigns[7];
                    case 9:
                        return BirthDate.Day >= 23 ? _westSigns[9] : _westSigns[8];
                    case 10:
                        return BirthDate.Day >= 23 ? _westSigns[10] : _westSigns[9];
                    case 11:
                        return BirthDate.Day >= 22 ? _westSigns[11] : _westSigns[10];
                    case 12:
                        return BirthDate.Day >= 22 ? _westSigns[0] : _westSigns[11];
                    default: return "";
                }
            }
        }

        public string ChineseSign
        {
            get
            {
                switch ((BirthDate.Year - 4) % 12)
                {
                    case 0:
                        return _chineseSigns[0];
                    case 1:
                        return _chineseSigns[1];
                    case 2:
                        return _chineseSigns[2];
                    case 3:
                        return _chineseSigns[3];
                    case 4:
                        return _chineseSigns[4];
                    case 5:
                        return _chineseSigns[5];
                    case 6:
                        return _chineseSigns[6];
                    case 7:
                        return _chineseSigns[7];
                    case 8:
                        return _chineseSigns[8];
                    case 9:
                        return _chineseSigns[9];
                    case 10:
                        return _chineseSigns[10];
                    case 11:
                        return _chineseSigns[11];
                    case 12:
                        return _chineseSigns[12];
                    default: return "";
                }
            }
        }

        public bool IsAdult
        {
            get
            {
                int age = 0;
                if (BirthDate.Month > DateTime.Now.Month ||
                    (BirthDate.Month == DateTime.Now.Month && BirthDate.Day > DateTime.Now.Day))
                {
                    age = DateTime.Now.Year - BirthDate.Year - 1;

                }
                else if (BirthDate.Month < DateTime.Now.Month ||
                         (BirthDate.Month == DateTime.Now.Month && BirthDate.Day <= DateTime.Now.Day))
                {
                    age = DateTime.Now.Year - BirthDate.Year;

                }

                return age >= 18;
            }
        }

        public bool IsBirthday
        {
            get
            {
                return (BirthDate.Day == DateTime.Now.Day && BirthDate.Month == DateTime.Now.Month);
            }
        }


        #endregion

        #region Constructors

        public User(string name, string surname, string email, DateTime dateBirth)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = dateBirth;
          
        }

        public User(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }

        public User(string name, string surname, DateTime dateBirth)
        {
            Name = name;
            Surname = surname;
            BirthDate = dateBirth;
        }

        #endregion


    }
    #region Exceptions

    class EmailException : Exception
    {
        public EmailException()
            : base("Invalid email!")
        {
        }
    }

    class FutureDateException : Exception
    {
        public FutureDateException()
            :base("Date is in future!")
        {
        }
    }

    class OldDateException : Exception
    {
        public OldDateException()
            : base("Date is too old!")
        {
        }
    }

    class InvalidNameException : Exception
    {
        public InvalidNameException()
            : base("Invalid name!")
        {
        }
    }
    #endregion
}
