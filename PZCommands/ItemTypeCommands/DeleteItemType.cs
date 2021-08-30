using DataAccess;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsItemType;

namespace PizzeriaCommands.ItemTypeCommands
{
    public class DeleteItemType : BaseCommand, IDeleteItemType
    {
        public DeleteItemType(PizzeriaContext context) : base(context)
        {

        }
        public void Execute(int req)
        {
            var delete = this.context.ItemTypes.Find(req);
            if (delete == null)
            {
                throw new ObjectDoesntExistException("ItemType");
            }
            else
            {
                delete.IsDeleted = true;
                this.context.SaveChanges();
            }
        }
    }
}
