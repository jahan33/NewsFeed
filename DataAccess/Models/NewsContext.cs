using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;


namespace DataAccess.Models
{
   public class NewsContext : DbContext
    {
        //public NewsContext(DbContextOptions<NewsContext> options)
        //    : base(options)
        //{ }
        // Set Connection String
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                .AddJsonFile("appsettings.json")
                                .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }
        public DbSet<tblChannel> tblChannel { get; set; }
        public DbSet<tblChannelType> tblChannelType { get; set; }
        public DbSet<tblNews> tblNews { get; set; }
        public DbSet<tblRole> tblRole { get; set; }
        public DbSet<tblUser> tblUser { get; set; }
        public DbSet<tblUserLog> tblUserLog { get; set; }
        public DbSet<tblPage> tblPage { get; set; }
        public DbSet<tblRolePage> tblRolePage { get; set; }
        public DbSet<tblUserRole> tblUserRole { get; set; }
        public DbSet<tblChannelSubscribe> tblChannelSubscribe { get; set; }
        
        // Seed data
        #region"Seed Data"
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region"Role"
            modelBuilder.Entity<tblRole>().HasData(
                new tblRole
                {
                    PKRole =1,
                    CreatedDate = DateTime.Now,
                    IsActive=true,
                    RoleName="Administrator"

                },
                 new tblRole
                 {
                     PKRole = 2,
                     CreatedDate = DateTime.Now,
                     IsActive = true,
                     RoleName = "Visitor"

                 }
            );
            #endregion
            #region"User"
            modelBuilder.Entity<tblUser>().HasData(
                new tblUser
                {
                    PKUser = 1,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    FullName = "Muhammad Jahangir Javed",
                    Email="jahangeer.javed@gmail.com",
                    Password= "NXo/ao4xL5ix30tACkl6jg=="

                },
                 new tblUser
                 {
                     PKUser = 2,
                     CreatedDate = DateTime.Now,
                     IsActive = true,
                     FullName = "Muhammad Jahangir Javed",
                     Email = "jahangeer.javed@gmail.com",
                     Password = "NXo/ao4xL5ix30tACkl6jg=="

                 }
            );
            #endregion
            #region"User Role"
            modelBuilder.Entity<tblUserRole>().HasData(
                new tblUserRole
                {
                    PKUserRole = 1,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    FKRole=1,
                    FKUser=1
                 

                },
                 new tblUserRole
                 {
                     PKUserRole = 2,
                     CreatedDate = DateTime.Now,
                     IsActive = true,
                     FKRole = 2,
                     FKUser = 2

                 }
            );
            #endregion
            #region"Channel Type"
            modelBuilder.Entity<tblChannelType>().HasData(
               new tblChannelType
               {
                   PKChannelType = 1,
                   CreatedDate = DateTime.Now,
                   IsActive = true,
                   TypeName = "Sports"
               },
                new tblChannelType
                {
                    PKChannelType = 2,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    TypeName = "Entertainment"
                }
                ,
                new tblChannelType
                {
                    PKChannelType = 3,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    TypeName = "National"
                }
                 ,
                new tblChannelType
                {
                    PKChannelType = 4,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    TypeName = "International"
                }
               );
            #endregion
            #region"Channel"
            modelBuilder.Entity<tblChannel>().HasData(
             
               new tblChannel
               {
                   PKChannel = 1,
                   CreatedDate = DateTime.Now,
                   IsActive = true,
                   ChannelName = "PTV Sport",
                   FKOwner = 1,
                   FKChannelType = 1,
                   ChannelDescription = "Sport Channel"
               },
                new tblChannel
                {
                    PKChannel = 2,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    ChannelName = "GEO",
                    FKOwner = 1,
                    FKChannelType = 2,
                    ChannelDescription = "Entertainment Channel"
                },
                 new tblChannel
                 {
                     PKChannel = 3,
                     CreatedDate = DateTime.Now,
                     IsActive = true,
                     ChannelName = "PTV News",
                     FKOwner = 1,
                     FKChannelType = 3,
                     ChannelDescription = "National Channel"
                 },
                 new tblChannel
                 {
                     PKChannel = 4,
                     CreatedDate = DateTime.Now,
                     IsActive = true,
                     ChannelName = "BBC",
                     FKOwner = 1,
                     FKChannelType = 4,
                     ChannelDescription = "International Channel"
                 }

              );
            #endregion
            #region"News"
            modelBuilder.Entity<tblNews>().HasData(

               new tblNews
               {
                   PKNews = 1,
                   CreatedDate = DateTime.Now.AddHours(-1),
                   IsActive = true,
                   FKChannel=1,
                   Author= "Football Italia Staff",
                   Title= "HT: Napoli on fire with Frosinone",
                   Description= "Piotr Zielinski&rsquo;s angled drive and an Adam Ounas scorcher have given Napoli the 2-0 half-time lead over Frosinone.",
                   Content= "Piotr Zielinski’s angled drive and an Adam Ounas scorcher have given Napoli the 2 - 0 half - time lead over Frosinone.Follow all the action as it happens from this game,Cagliari v Roma and Lazio v Sampdoria on the LIVEBLOG.The Partenopei had the opportunity to… [+2067 chars]"
                  ,ImageURL="/assets/images/SP1.jpg"
               },
                new tblNews
                {
                    PKNews = 2,
                    CreatedDate = DateTime.Now.AddHours(-2),
                    IsActive = true,
                    FKChannel = 1,
                    Author = "Football Italia Staff",
                    Title = "Liveblog: Serie A Super Saturday",
                    Description = "Join us for the build-up and action as it happens from today&rsquo;s Serie A games, as Napoli host Frosinone, Roma visit Cagliari and unpredictable Lazio v Sampdoria.",
                    Content = "Join us for the build-up and action as it happens from today’s Serie A games, as Napoli host Frosinone, Roma visit Cagliari and unpredictable Lazio v Sampdoria. If you are on a mobile device or tablet, then follow the Liveblog HERE. We begin at 14.00 GMT, as … [+1408 chars]"
                    ,ImageURL = "/assets/images/SP2.jpg"
                },
                 new tblNews
                 {
                     PKNews = 3,
                     CreatedDate = DateTime.Now.AddHours(-3),
                     IsActive = true,
                     FKChannel = 1,
                     Author = "Football Italia Staff",
                     Title = "Ghoulam: 'Proud to represent Napoli'",
                     Description = "Faouzi Ghoulam thanked Napoli, his teammates and the fans after a spectacular comeback 402 days on from his injury. “I’m proud to represent this city.”",
                     Content = "Faouzi Ghoulam thanked Napoli, his teammates and the fans after a spectacular comeback 402 days on from his injury. “I’m proud to represent this city.” The Algeria international hadn’t played since tearing his ACL in November 2017, but started today’s 4-0 Ser… [+1150 chars]"
                     ,ImageURL = "/assets/images/SP3.jpg"
                 },
                  new tblNews
                  {
                      PKNews = 4,
                      CreatedDate = DateTime.Now.AddHours(-4),
                      IsActive = true,
                      FKChannel = 2,
                      Author = "James Hibberd",
                      Title = "Jason Momoa wants to see the Zach Snyder Cut of <em>Justice League</em>",
                      Description = "Jason Momoa is weighing in on the mysterious Zach Snyder Cut of 'Justice League.'",
                      Content = "Jason Momoa is weighing in on the mysterious Zach Snyder Cut of Justice League. The Aquaman star was asked by MTV News about the director’s original edit of the divisive 2017 superhero film, which has become a target of fanboy fascination who have lobbied for… [+1947 chars]"
                      ,ImageURL = "/assets/images/IN1.jpg"
                  },
                   new tblNews
                   {
                       PKNews = 5,
                       CreatedDate = DateTime.Now.AddHours(-5),
                       IsActive = true,
                       FKChannel = 2,
                       Author = "Esme Douglas",
                       Title = "Candace Cameron Bure hospitalized after go-karting accident with brother Kirk",
                       Description = "Candace Cameron Bure hospitalized after go-karting accident with brother Kirk ew.com",
                       Content = "The holidays can be stressful, especially if you get into a go-kart accident with your sibling. Fuller House star Candace Cameron Bure documented her recent trip to the hospital after a fun family activity landed her in the emergency room, telling her followe… [+854 chars]"
                       ,ImageURL = "/assets/images/IN2.jpg"
                   },
                    new tblNews
                    {
                        PKNews = 6,
                        CreatedDate = DateTime.Now.AddHours(-6),
                        IsActive = true,
                        FKChannel = 2,
                        Author = "Nick Romano",
                        Title = "Sister Act 3 from Insecure, Star producers in the works for Disney+",
                        Description = "Sister Act 3 from Insecure, Star producers in the works for Disney+ ew.com",
                        Content = "Anyone with very specific memories of shouting “Salve Regina” at the top of their lungs while on a middle school choir field trip might find this news particularly interesting: 25 years after Sister Act 2: Back in the Habit debuted with one of the best sequel… [+2284 chars]"
                        ,ImageURL = "/assets/images/IN3.jpg"
                    },
                     new tblNews
                     {
                         PKNews = 7,
                         CreatedDate = DateTime.Now.AddHours(-7),
                         IsActive = true,
                         FKChannel = 3,
                         Author = "Joan Biskupic, CNN legal analyst & Supreme Court biographer",
                         Title = "This justice began the Supreme Court's conservative transformation",
                         Description = "When Chief Justice John Roberts rebuked President Donald Trump over judicial independence in November, some critics countered with a 2010 moment between President Barack Obama and Justice Samuel Alito.",
                         Content = "When Chief Justice John Roberts rebuked President Donald Trump over judicial independence in November, some critics countered with a 2010 moment between President Barack Obama and Justice Samuel Alito."
                         ,ImageURL = "/assets/images/N1.jpg"
                     },
                      new tblNews
                      {
                          PKNews = 8,
                          CreatedDate = DateTime.Now.AddHours(-8),
                          IsActive = true,
                          FKChannel = 3,
                          Author = "Jeremy Herb, Katelyn Polantz and Marshall Cohen, CNN",
                          Title = "Takeaways from the new Cohen and Manafort filings",
                          Description = "Friday was a very, very bad day for Donald Trump",
                          Content = "On Friday, President Donald Trump (and the rest of us) got the most fulsome look into the inner workings of special counsel Robert Mueller's investigation yet. And what Trump saw has to terrify him."
                          ,ImageURL = "/assets/images/N2.jpg"
                      },
                       new tblNews
                       {
                           PKNews = 9,
                           CreatedDate = DateTime.Now.AddHours(-9),
                           IsActive = true,
                           FKChannel = 3,
                           Author = "Erica Orden and Marshall Cohen",
                           Title = "Prosecutors: Michael Cohen acted at Trump's direction when he broke the law",
                           Description = "Federal prosecutors said for the first time Friday that Michael Cohen acted at the direction of Donald Trump when the former fixer committed two election-related crimes during the 2016 presidential campaign, as special counsel Robert Mueller outlined a previo…",
                           Content = "New York (CNN) Federal prosecutors said for the first time Friday that Michael Cohen acted at the direction of Donald Trump when the former fixer committed two election-related crimes during the 2016 presidential campaign, as special counsel Robert Mueller ou… [+8884 chars]"
                           ,ImageURL = "/assets/images/N3.jpg"
                       },
                        new tblNews
                        {
                            PKNews = 10,
                            CreatedDate = DateTime.Now.AddDays(-1),
                            IsActive = true,
                            FKChannel = 4,
                            Author = "BBC News",
                            Title = "Missing mosaic pieces returned to Turkey",
                            Description = "Fragments of the ancient Gypsy Girl mosaic given back by a US university go on show at a museum.",
                            Content = "Image copyright Reuters Image caption The missing pieces were smuggled out of Turkey in the 1960s Missing fragments of an ancient mosaic known as the Gypsy Girl have been put on display in a museum in Turkey after being returned from the United States. The fr… [+987 chars]"
                            ,ImageURL = "/assets/images/BBC1.jpg"
                        },
                         new tblNews
                         {
                             PKNews = 11,
                             CreatedDate = DateTime.Now.AddDays(-2),
                             IsActive = true,
                             FKChannel = 4,
                             Author = "BBC News",
                             Title = "Algeria beatifications are first in a Muslim nation",
                             Description = "The first such ceremony in a Muslim nation was for 19 Catholics killed in Algeria's civil war",
                             Content = "Image copyright EPA Image caption The beatification took place in Oran's Chapel of our Lady of Santa Cruz The Catholic Church has beatified 19 Catholics killed in Algeria's civil war - the first such ceremony in a Muslim nation. Most of them were French - sev… [+2212 chars]"
                             ,ImageURL = "/assets/images/BBC2.jpg"
                         },
                          new tblNews
                          {
                              PKNews = 12,
                              CreatedDate = DateTime.Now,
                              IsActive = true,
                              FKChannel = 4,
                              Author = "Alen",
                              Title = "Napoli cruise to easy Frosinone win",
                              Description = "Napoli prepare for their decisive Champions League game at Liverpool with a 4-0 victory over struggling Frosinone in Serie A.",
                              Content = "At 25 years and 243 days, the average age of Napoli's starting team was its youngest in Serie A since September 2009 Napoli prepared for their decisive Champions League game at Liverpool on Tuesday with an impressive home victory over struggling Frosinone in … [+641 chars]"
                              ,ImageURL = "/assets/images/BBC3.jpg"
                          }
               );
            #endregion
        }
        #endregion

    }
}
