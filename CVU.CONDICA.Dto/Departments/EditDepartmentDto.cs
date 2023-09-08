using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVU.CONDICA.Dto.Positions
{
    public class EditPositionDto
    {
        public EditPositionDto() { }
        public EditPositionDto(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
