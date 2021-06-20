using Microsoft.Extensions.Configuration;
using Payment.Solution.DataAccessLayer.Interfaces;
using Payment.Solution.RuntimeModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Payment.Solution.DataAccessLayer.Repositories
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        private readonly IConfiguration _configuration;

        public InvoiceRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object Create(Invoice entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invoice> Get(string where = null)
        {
            var invoiceList = new List<Invoice> { };

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("usp_SEL_Invoice", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Filter", where);

                    con.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        invoiceList.Add(new Invoice
                        {
                            Id = Convert.ToInt32(reader[nameof(Invoice.Id)]),
                            Amount = Convert.ToDecimal(reader[nameof(Invoice.Amount)]),
                            PaymentStatus = Convert.ToBoolean(reader[nameof(Invoice.PaymentStatus)]),
                            UserId = Convert.ToInt32(reader[nameof(Invoice.UserId)]),
                            IdentityNumber = Convert.ToString(reader[nameof(Invoice.IdentityNumber)])
                        });
                    }
                }
            }

            return invoiceList;
        }

        public void Update(string where, string set)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand($@"UPDATE I
                                                          SET {set}
                                                          FROM tbl_Invoice I
                                                          WHERE {where}", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
