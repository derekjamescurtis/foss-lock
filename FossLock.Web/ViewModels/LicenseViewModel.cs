using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FossLock.Core;

namespace FossLock.Web.ViewModels
{
    public class LicenseViewModel : IFossLockViewModel
    {
        public LicenseViewModel()
        {
            AllActivationTypes = new SelectList(
                ((IEnumerable<int>)Enum.GetValues(typeof(ActivationType)))
                .Where(e => (ActivationType)e != ActivationType.None)
                .Select(e =>
                    new
                    {
                        Text = Enum.GetName(typeof(ActivationType), e),
                        Value = e.ToString()
                    }), "Value", "Text"
                );

            AllLockProperties = new SelectList(
                ((IEnumerable<int>)Enum.GetValues(typeof(LockPropertyType)))
                .Where(e => (LockPropertyType)e != LockPropertyType.None)
                .Select(e =>
                    new
                    {
                        Text = Enum.GetName(typeof(LockPropertyType), e),
                        Value = e.ToString()
                    }), "Value", "Text"
                );
        }

        public int Id { get; set; }

        public DateTimeOffset GenerationDateTime { get; set; }

        public DateTimeOffset DestroyedDateTime { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public SelectList AllActivationTypes { get; set; }

        public SelectList AllLockProperties { get; set; }
    }
}