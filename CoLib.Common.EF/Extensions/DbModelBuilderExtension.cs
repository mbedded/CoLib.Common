using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Pluralization;

namespace CoLib.Common.EF.Extensions {

  /// <summary>
  ///   This class contains Extensions for DbModelBuilder.
  /// </summary>
  public static class DbModelBuilderExtension {

    /// <summary>
    ///   This method can be used to use prefixes in the DB-Context.
    ///   The EF-Default will be used which means the plural of the class name
    ///   will be used as table name.
    ///
    ///   Example:
    ///     Prefix = MyValue
    ///     Result = MyValue_TableName
    /// </summary>
    /// <param name="xBuilder">The current DbModelBuilder</param>
    /// <param name="xPrefix">The prefix to use.</param>
    public static void UseTablePrefix(this DbModelBuilder xBuilder, string xPrefix) {
      xBuilder.Types().Configure(conf => conf.ToTable(GetTableName(conf.ClrType, xPrefix)));
    }

    private static string GetTableName(Type xType, string xPrefix) {
      IPluralizationService pluralizationService = DbConfiguration.DependencyResolver.GetService<IPluralizationService>();
      string result = pluralizationService.Pluralize(xType.Name);

      return $"{xPrefix}_{result}";
    }

  }

}
