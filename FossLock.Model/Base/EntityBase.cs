﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model.Base.SharpArchitecture;

namespace FossLock.Model.Base
{
    public abstract class EntityBase : EntityWithTypedId<int>, IEntityBase
    {
    }
}