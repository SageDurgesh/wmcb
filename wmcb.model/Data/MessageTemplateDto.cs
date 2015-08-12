using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.Data
{
    public class MessageTemplateDto
    {
        public int ID { get; set; }
        public string MessageType { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
