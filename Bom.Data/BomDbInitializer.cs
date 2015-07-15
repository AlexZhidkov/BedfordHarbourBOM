using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Entities;
using Bom.Common;
using Bom.Data.Migrations;

namespace Bom.Data
{
    //ToDo Remove this class
    public class BomDbInitializer : MigrateDatabaseToLatestVersion<BomContext, Configuration>
    {
    }
}
