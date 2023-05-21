using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Models;

namespace HealthInsurance.Data
{
    public class HealthInsuranceContext : DbContext
    {
        public HealthInsuranceContext (DbContextOptions<HealthInsuranceContext> options)
            : base(options)
        {
        }

        public DbSet<HealthInsurance.Models.Employee> Employees { get; set; } = default!;

        public DbSet<HealthInsurance.Models.Hospital>? Hospitals { get; set; }

        public DbSet<HealthInsurance.Models.Policy>? Policies { get; set; }

        public DbSet<HealthInsurance.Models.PolicyOnEmp>? PolicyOnEmp { get; set; }
    }
}
