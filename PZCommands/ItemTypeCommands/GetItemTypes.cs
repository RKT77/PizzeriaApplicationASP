using DataAccess;
using PizzeriaApplication.DTO;
using PizzeriaApplication.ICommands.ICommandsItemType;
using PizzeriaApplication.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.ItemTypeCommands
{
    public class GetItemTypes : BaseCommand, IGetItemTypes
    {
        public GetItemTypes(PizzeriaContext context) : base(context)
        {

        }
        public IEnumerable<ItemTypeDTO> Execute(ItemTypeSearch req)
        {
                var Types = this.context.ItemTypes.AsQueryable().Where(p=>p.IsDeleted==false);
            
                if (req.Name != null)
                {
                    Types = Types.Where(p => p.Name.ToLower().Contains(req.Name.ToLower()));
                }
                return Types.Select(p => new ItemTypeDTO
                {
                    Id=p.Id,
                    Name=p.Name
                });
          
        }
    }
}
