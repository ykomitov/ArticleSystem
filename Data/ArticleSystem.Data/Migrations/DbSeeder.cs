namespace ArticleSystem.Data.Migrations
{
    using System;
    using System.Linq;
    using ArticleSystem.Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Tools.ImageConverter;

    public static class DbSeeder
    {
        public static void SeedUsers(ApplicationDbContext context)
        {
            const string AdministratorEmail = "admin@admin.com";
            const string AdministratorUserName = "admin";
            const string PowerUserEmail = "power@user.com";
            const string PowerUserUserName = "powerUser";
            const string NormalUserEmail = "normal@user.com";
            const string NormalUserUserName = "normalUser";

            if (!context.Roles.Any())
            {
                // Create roles
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var adminRole = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
                roleManager.Create(adminRole);

                var powerUserRole = new IdentityRole { Name = GlobalConstants.PowerUserRoleName };
                roleManager.Create(powerUserRole);

                var normalUserRole = new IdentityRole { Name = GlobalConstants.NormalUserRoleName };
                roleManager.Create(normalUserRole);

                // Create users
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var admin = new ApplicationUser { UserName = AdministratorUserName, Email = AdministratorEmail };
                userManager.Create(admin, AdministratorEmail);

                var powerUser = new ApplicationUser { UserName = PowerUserUserName, Email = PowerUserEmail };
                userManager.Create(powerUser, PowerUserEmail);

                var normalUser = new ApplicationUser { UserName = NormalUserUserName, Email = NormalUserEmail };
                userManager.Create(normalUser, NormalUserEmail);

                // Assign users to roles
                userManager.AddToRole(admin.Id, GlobalConstants.AdministratorRoleName);
                userManager.AddToRole(powerUser.Id, GlobalConstants.PowerUserRoleName);
                userManager.AddToRole(normalUser.Id, GlobalConstants.NormalUserRoleName);
            }
        }

        public static void SeedCategories(ApplicationDbContext context)
        {
            string[] categories = new string[]
            {
                    "News",
                    "Tech",
                    "Devices",
                    "Soft",
                    "VLog",
                    "About"
            };

            if (!context.ArticleCategories.Any())
            {
                for (int i = 0; i < categories.Length; i++)
                {
                    var newCategory = new ArticleCategory()
                    {
                        Name = categories[i]
                    };

                    context.ArticleCategories.Add(newCategory);
                }
            }

            context.SaveChanges();
        }

        public static void SeedArticles(ApplicationDbContext context)
        {
            const string sliderImageArticle1 = "chameleon-1.jpeg";
            const string headerImageArticle1 = "img-s-1.jpg";
            const string sliderImageArticle2 = "chameleon-2.jpeg";
            const string headerImageArticle2 = "img-s-2.jpg";
            const string sliderImageArticle3 = "chameleon-3.jpeg";
            const string sliderImageArticle4 = "chameleon-4.jpeg";

            if (!context.Articles.Any())
            {
                var imageConverter = new ImageConverter();
                var pathBase = AppDomain.CurrentDomain.BaseDirectory + "../ArticleSystem.Web/images/";

                /*  var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
                    var directoryName = Path.GetDirectoryName(absolutePath);*/

                var sampleArticle1 = new Article()
                {
                    Title = "Google is experimenting with article recommendations in Chrome",
                    TextHtml = "<p>Google is working on a feature that would recommend articles directly in its browser. The suggestions would appear on the new tab page in Chrome for Windows, Mac, Linux, Android, and iOS.</p><p>Google confirmed it is still testing the feature, which is not yet available. “We’re always experimenting with new features in Chrome, but have nothing new to announce at this time,” a Google spokesperson told VentureBeat.</p><p>It’s not clear what the new functionality will be called, though multiple tickets on Chromium Code Reviews mention a “Morning Reads” service and a “ChromeReader” feature</p><p>Based on what we’ve seen, it appears Google wants to bring something like the article recommendations in Google Now directly into its browser. While it’s definitely interesting to see what Google is playing with, this is still very early days. The feature isn’t even available in Chromium or Chrome Canary, and because development is still ongoing, it could be killed at any time.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + sliderImageArticle1),
                    SliderImageFileName = sliderImageArticle1,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + headerImageArticle1),
                    HeaderImageFileName = headerImageArticle1,
                    Category = context.ArticleCategories.Where(c => c.Id == 1).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle1);
                context.SaveChanges();

                var sampleArticle2 = new Article()
                {
                    Title = "Robots could learn human values by reading stories",
                    TextHtml = "<p>More than 70 years ago, Isaac Asimov dreamed up his three laws of robotics, which insisted, above all, that “a robot may not injure a human being or, through inaction, allow a human being to come to harm”. Now, after <a href='https://www.theguardian.com/science/2014/dec/02/stephen-hawking-intel-communication-system-astrophysicist-software-predictive-text-type'>Stephen Hawking warned </a>that “the development of full artificial intelligence could spell the end of the human race”, two academics have come up with a way of teaching ethics to computers: telling them stories.</p><p>Mark Riedl and Brent Harrison from the School of Interactive Computing at the Georgia Institute of Technology have just unveiled Quixote, a prototype system that is able to learn social conventions from simple stories. Or, as they put in their paper Using Stories to Teach Human Values to Artificial Agents, revealed at the AAAI-16 Conference in Phoenix, Arizona this week, the stories are used “to generate a value-aligned reward signal for reinforcement learning agents that prevents psychotic-appearing behaviour”.</p><p>A simple version of a story could be about going to get prescription medicine from a chemist, laying out what a human would typically do and encounter in this situation. An AI (artificial intelligence) given the task of picking up a prescription for a human could, variously, rob the chemist and run, or be polite and wait in line. Robbing would be the fastest way to accomplish its goal, but Quixote learns that it will be rewarded if it acts like the protagonist in the story.</p><p>“The AI … runs many thousands of virtual simulations in which it tries out different things and gets rewarded every time it does an action similar to something in the story,” said Riedl, associate professor and director of the Entertainment Intelligence Lab. “Over time, the AI learns to prefer doing certain things and avoiding doing certain other things. We find that Quixote can learn how to perform a task the same way humans tend to do it. This is significant because if an AI were given the goal of simply returning home with a drug, it might steal the drug because that takes the fewest actions and uses the fewest resources. The point being that the standard metrics for success (eg, efficiency) are not socially best.”</p><p>Quixote has not learned the lesson of “do not steal”, Riedl says, but “simply prefers to not steal after reading and emulating the stories it was provided”</p><p>“I think this is analogous to how humans don’t really think about the consequences of their actions, but simply prefer to follow the conventions that we have learned over our lifetimes,” he added. “Another way of saying this is that the stories are surrogate memories for an AI that cannot ‘grow up’ immersed in a society the way people are and must quickly immerse itself in a society by reading about [it].”</p><p>“As the use of AI becomes more prevalent in our society, and as AI becomes more capable, the consequences of their actions become more significant. Giving AIs the ability to read and understand stories may be the most expedient means of enculturing [them] so that they can better integrate themselves into human societies and contribute to our overall wellbeing,” they conclude.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + sliderImageArticle2),
                    SliderImageFileName = sliderImageArticle2,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + headerImageArticle2),
                    HeaderImageFileName = headerImageArticle2,
                    Category = context.ArticleCategories.Where(c => c.Id == 1).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle2);
                context.SaveChanges();

                var sampleArticle3 = new Article()
                {
                    Title = "Google cleans up search results by ditching sidebar ads",
                    TextHtml = "<p>Google generates a huge amount of revenue through advertising but it's not afraid to try mixing things up a little. Ads in search results have long-been controversial, but the latest change is likely to go down well with many people -- the ads that currently appear in the right-hand sidebar of search results are to be dropped.</p><p>The change means that ads will only be displayed above and below search results. There will be seven Google AdWords ads in total -- four above the search results and three below. The right-hand side of the page will be left free for Google's own Product Listing Ads. Google also confirmed that the change is global and affects all languages.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + sliderImageArticle3),
                    SliderImageFileName = sliderImageArticle3,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + headerImageArticle1),
                    HeaderImageFileName = headerImageArticle1,
                    Category = context.ArticleCategories.Where(c => c.Id == 1).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle3);
                context.SaveChanges();

                var sampleArticle4 = new Article()
                {
                    Title = "WFP uses innovative iris scan technology to provide food assistance to syrian refugees in Jordan",
                    TextHtml = "<p>Today, a Syrian refugee at King Abdullah Park refugee camp in northern Jordan walked into a supermarket to redeem her monthly food assistance and instead of using her usual prepaid electronic card at the checkout, she looked into an iris scan camera, and paid for her shopping in the twinkling of an eye.</p><p>Today, a Syrian refugee at King Abdullah Park refugee camp in northern Jordan walked into a supermarket to redeem her monthly food assistance and instead of using her usual prepaid electronic card at the checkout, she looked into an iris scan camera, and paid for her shopping in the twinkling of an eye.</p><p>“This a milestone in the evolution of our food assistance programme, which has come so far from the first few months of the Syria crisis, when we distributed food parcels,” said WFP Country Director in Jordan Mageed Yahia.</p><p>With WFP’s iris scan system, we are perfecting the delivery of food assistance, becoming more efficient, enhancing accountability and making grocery shopping easier and more secure for the refugees. The fact that this is happening in Jordan makes it all the more exciting as we hope that it will further contribute to the country’s progress towards being a regional hub for technology.”</p><p>WFP’s innovative system relies on the UN Refugee Agency’s biometric registration data of refugees and works with WFP’s Jordanian partners; IrisGuard, the company that developed the iris scan platform, Jordan Ahli Bank and their counterpart Middle East Payment Systems (MEPS).</p><p>Once the shopper has their iris scanned, the system automatically communicates with UNHCR’s registration database to confirm the identity of the refugee, before automatically continuing to the Jordan Ahli Bank through the Middle East Payment System financial gateway to determine the refugee’s remaining balance. It then confirms the purchase and prints a receipt for the refugee.</p><p>WFP is looking to expand the use of its new iris scan payment system for refugees living in all Syrian refugee camps in Jordan during the coming months. Depending on the success of the system in the camps, WFP may also expand the use of this technology in areas outside of the camps.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + sliderImageArticle4),
                    SliderImageFileName = sliderImageArticle4,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + headerImageArticle1),
                    HeaderImageFileName = headerImageArticle1,
                    Category = context.ArticleCategories.Where(c => c.Id == 2).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle4);
                context.SaveChanges();

                var sampleArticle5 = new Article()
                {
                    Title = "The legal question at the core of the Apple encryption standoff",
                    TextHtml = "<p>Apple CEO Tim Cook claims that if his company helps the FBI get into a dead terrorist’s iPhone, a precedent would be set that has “implications far beyond” this case. But is that true in the legal sense?</p><p>Technologically speaking, it should be possible for Apple to cooperate with law enforcement—as telecommunications companies have done for 100 years—in this particular case without compromising the tough encryption standards now baked into its phones. So the question then becomes: would this cooperation actually set an unwanted legal precedent for Apple?</p><p>Apple contends the feds are seeking an “unprecedented use” of the All Writs Act, a 1789 law that allows courts to issue orders that pertain to matters not yet covered by a law. But we’re not exactly in uncharted waters. As Kate Knibbs wrote on Gizmodo, the government has leaned several times on the All Writs Act to compel Apple and other communications companies to facilitate investigations. At least one judge has rejected a request on the grounds that the government can’t use the 1789 act to justify pretty much anything that Congress hasn’t yet explicitly allowed or disallowed.</p><p>Now Apple is betting that it can persuade a different judge to see things the same way—and that it's not, as the Justice Department contended on Friday, merely trying to protect its “business model and public brand marketing strategy.”</p><p>So how likely is it that company wins this case? It's very hard to predict, says Orin Kerr, a professor at George Washington University School of Law who specializes in the laws governing computer crime. Kerr wrote in the Washington Post that previous rulings don't match well onto this case, largely because Apple is not a perfect analogue for the traditional telephone companies involved in past surveillance practices. “This case is like a crazy - hard law school exam hypothetical in which a professor gives students an unanswerable problem just to see how they do,” Kerr wrote.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + sliderImageArticle1),
                    SliderImageFileName = sliderImageArticle1,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + headerImageArticle2),
                    HeaderImageFileName = headerImageArticle2,
                    Category = context.ArticleCategories.Where(c => c.Id == 2).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle5);
                context.SaveChanges();

                var sampleArticle6 = new Article()
                {
                    Title = "Global wind power capacity tops nuclear energy for first time",
                    TextHtml = "<p>The capacity of wind power generation worldwide reached 432.42 gigawatts (GW) at the end of 2015, up 17 percent from a year earlier and surpassing nuclear energy for the first time, according to data released by global industry bodies.</p><p>The generation capacity of wind farms newly built in 2015 was a record 63.01 GW, corresponding to about 60 nuclear reactors, according to the Global Wind Energy Council based in Brussels. The global nuclear power generation capacity was 382.55 GW as of Jan. 1, 2016, the London-based World Nuclear Association said.</p><p>Both wind power and nuclear energy are being touted as alternatives to fossil fuel power as they produce fewer greenhouse gases.</p><p>Wind energy has captured renewed attention as technological innovation has considerably lowered its generation costs while nuclear power continues to suffer a backlash following the 2011 Fukushima meltdowns.</p><p>Wind power is the leading energy source in the transition from fossil fuels to renewables, the wind energy council said as it released the data last week.</p><p>China led all other countries in wind energy generation capacity with 145.10 GW. Beijing is promoting wind power to shift from coal and other fossil fuels to combat air pollution and global warming.</p><p>Coming in second behind China is the United States with 74.47 GW, followed by Germany with 44.95 GW, then India with 25.09 GW and then Spain with 23.03 GW. Japan produced 3.04 GW.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + sliderImageArticle2),
                    SliderImageFileName = sliderImageArticle2,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + headerImageArticle1),
                    HeaderImageFileName = headerImageArticle1,
                    Category = context.ArticleCategories.Where(c => c.Id == 2).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle6);
                context.SaveChanges();

                var sampleArticle7 = new Article()
                {
                    Title = "Apple says sorry for iPhone error 53 and issues IOS 9.2.1 update to fix it",
                    TextHtml = "<p>Apple has a lot of support at the moment for its stance on encryption and refusing the FBI access to an iPhone's contents, but it's only a couple of weeks since the company was seen in a less favorable light. There was quite a backlash when users found that installing an update to iOS resulted in Error 53 and a bricked iPhone. Apple initially said that Error 53 was caused 'for security reasons' following speculation that it was a bid to stop people from using third party repair shops. iFixit suggested that the problem was a result of a failure of parts to correctly sync, and Apple has been rounding criticized for failing to come up with a fix. Today the company has issued an apology, along with an update that ensures Error 53 won't happen again. But there's more good news ... If you were talked into paying for an out of warranty replacement as a result of Error 53, you could be in line to get your money back.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + sliderImageArticle3),
                    SliderImageFileName = sliderImageArticle3,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + headerImageArticle2),
                    HeaderImageFileName = headerImageArticle2,
                    Category = context.ArticleCategories.Where(c => c.Id == 3).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle7);
                context.SaveChanges();
            }
        }
    }
}
