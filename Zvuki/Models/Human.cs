using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class Human
    {
        [Key]
        public int IdHuman { get; set; }

        string name, surname, patronomic, phone, login, email, password;
        DateTime dateOfBirth;

        [Required]
        [StringLength(50)]
        public string Name 
        { 
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        [Required]
        [StringLength(50)]
        public string Surname 
        { 
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }

        [Required]
        [StringLength(50)]
        public string Patronomic 
        { 
            get { return patronomic; }
            set
            {
                patronomic = value;
                OnPropertyChanged("Patronomic");
            }
        }


        [Required]
        [StringLength(50, MinimumLength = 8)]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$")]
        public string Phone 
        { 
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        [Required]
        [StringLength(50, MinimumLength = 3)]
    
        public string Login 
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        [StringLength(50, MinimumLength = 3)]
        public string Email 
        { 
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        [Required]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password 
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        
        }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }
        public bool isAdmin { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
