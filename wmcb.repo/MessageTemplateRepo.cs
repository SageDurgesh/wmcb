using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model.Data;

namespace wmcb.repo
{
    public class MessageTemplateRepo
    {
        public MessageTemplateDto getMessageTemplate(string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                using (var context = new wmcbContext())
                {
                    var template = context.MessageTemplate.Where(m => m.MessageType.Equals(type, StringComparison.CurrentCultureIgnoreCase));
                    return template.FirstOrDefault();
                }
            }
            return null;
        }
    }
}
