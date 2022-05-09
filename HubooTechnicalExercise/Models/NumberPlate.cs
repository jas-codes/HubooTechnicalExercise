using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubooTechnicalExercise
{
    public class NumberPlate
    {
        private int _validLength = 7;
        private string _registration = string.Empty;

        public string Registration {
            get => _registration;
            set => _registration = value?.Replace(" ", "");
        }

        public bool IsPlateRegistrationValid => !string.IsNullOrWhiteSpace(Registration)
            && !Registration.Any(c => !Char.IsLetterOrDigit(c)) 
            && Registration.Length <= _validLength;
    }
}
