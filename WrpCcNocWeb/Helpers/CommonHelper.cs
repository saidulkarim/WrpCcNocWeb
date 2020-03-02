using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Helpers
{
    public class CommonHelper
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        public CommonHelper()
        {

        }

        //public SelectList GetWaterQualityParams()
        //{
        //    SelectList origList = new SelectList(db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false), "WaterQualityParamID", "ParameterName", "");

        //    List<SelectListItem> newList = origList.ToList();
        //    newList.Insert(0, new SelectListItem() { Value = "", Text = "Select an option..." });

        //    var selectedItem = newList.FirstOrDefault(item => item.Selected);
        //    var selectedItemValue = String.Empty;
        //    if (selectedItem != null)
        //    {
        //        selectedItemValue = selectedItem.Value;
        //    }

        //    return new SelectList(newList, "Value", "Text", selectedItemValue);
        //}

        public enum Month
        {
            January,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        public enum Sign
        {
            Danger,
            Dark,
            Default,
            Error,
            Info,
            Primary,
            Success,
            Warning
        }

        public enum OperationMessage
        {
            [DisplayText("Information has been deleted.")]
            Delete,

            [DisplayText("Information not deleted.")]
            NotDelete,

            [DisplayText("An error has been occured!")]
            Error,

            [DisplayText("Data has been saved successfully.")]
            Success,

            [DisplayText("Data not save.")]
            NotSuccess,

            [DisplayText("Data has been updated successfully.")]
            Update,

            [DisplayText("Data not updated.")]
            NotUpdate,

            [DisplayText("Data has been imported successfully.")]
            ImportSuccess,

            [DisplayText("Data not imported successfully.")]
            ImportNotSuccess
        }

        public string ShowMessage(Enum sign, string msg)
        {
            return sign + ": " + msg;
        }

        public string ShowMessage(Enum sign, string title, string msg)
        {
            return sign + ": " + msg + "###" + title;
        }

        public string ExtractInnerException(Exception ex)
        {
            string message = string.Empty;
            if (ex.Message.Contains("See the inner exception"))
            {
                if (ex.InnerException.Message.Contains("See the inner exception"))
                {
                    message = ex.InnerException.InnerException.Message;
                }
                else
                {
                    message = ex.InnerException.Message.ToString();
                }
            }
            else
            {
                message = ex.Message.ToString();
            }

            return message.Replace("'", "").Replace("\"", "").Replace("\r\n", "").Replace("\n", "");
        }
    }
}
