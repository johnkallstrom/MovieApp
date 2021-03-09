using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieApp.Domain.Validation
{
    public class EnsureOneElement : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;

            if (list != null)
            {
                if (list.Count > 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
