using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.ViewModels
{
    public class HomeVM
    {
        public List<Cart> Carts { get; set; }
        public Setting Settings { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Featured> Featureds { get; set; }
        public List<Client> Clients { get; set; }
    }
}
