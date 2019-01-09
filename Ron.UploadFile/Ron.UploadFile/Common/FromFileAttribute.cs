using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.UploadFile.Common
{
    public class FromFileAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource => BindingSource.FormFile;
    }
}
