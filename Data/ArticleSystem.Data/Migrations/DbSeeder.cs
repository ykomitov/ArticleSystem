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
                    "Science",
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
            const string SliderImageArticle1 = "chameleon-1.jpeg";
            const string HeaderImageArticle1 = "img-s-1.jpg";
            const string SliderImageArticle2 = "chameleon-2.jpeg";
            const string HeaderImageArticle2 = "img-s-2.jpg";
            const string SliderImageArticle3 = "chameleon-3.jpeg";
            const string SliderImageArticle4 = "chameleon-4.jpeg";

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
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + SliderImageArticle1),
                    SliderImageFileName = SliderImageArticle1,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + HeaderImageArticle1),
                    HeaderImageFileName = HeaderImageArticle1,
                    Category = context.ArticleCategories.Where(c => c.Id == 1).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle1);
                context.SaveChanges();

                var sampleArticle2 = new Article()
                {
                    Title = "Robots could learn human values by reading stories",
                    TextHtml = "<p>More than 70 years ago, Isaac Asimov dreamed up his three laws of robotics, which insisted, above all, that “a robot may not injure a human being or, through inaction, allow a human being to come to harm”. Now, after <a href='https://www.theguardian.com/science/2014/dec/02/stephen-hawking-intel-communication-system-astrophysicist-software-predictive-text-type'>Stephen Hawking warned </a>that “the development of full artificial intelligence could spell the end of the human race”, two academics have come up with a way of teaching ethics to computers: telling them stories.</p><p>Mark Riedl and Brent Harrison from the School of Interactive Computing at the Georgia Institute of Technology have just unveiled Quixote, a prototype system that is able to learn social conventions from simple stories. Or, as they put in their paper Using Stories to Teach Human Values to Artificial Agents, revealed at the AAAI-16 Conference in Phoenix, Arizona this week, the stories are used “to generate a value-aligned reward signal for reinforcement learning agents that prevents psychotic-appearing behaviour”.</p><p>A simple version of a story could be about going to get prescription medicine from a chemist, laying out what a human would typically do and encounter in this situation. An AI (artificial intelligence) given the task of picking up a prescription for a human could, variously, rob the chemist and run, or be polite and wait in line. Robbing would be the fastest way to accomplish its goal, but Quixote learns that it will be rewarded if it acts like the protagonist in the story.</p><p>“The AI … runs many thousands of virtual simulations in which it tries out different things and gets rewarded every time it does an action similar to something in the story,” said Riedl, associate professor and director of the Entertainment Intelligence Lab. “Over time, the AI learns to prefer doing certain things and avoiding doing certain other things. We find that Quixote can learn how to perform a task the same way humans tend to do it. This is significant because if an AI were given the goal of simply returning home with a drug, it might steal the drug because that takes the fewest actions and uses the fewest resources. The point being that the standard metrics for success (eg, efficiency) are not socially best.”</p><p>Quixote has not learned the lesson of “do not steal”, Riedl says, but “simply prefers to not steal after reading and emulating the stories it was provided”</p><p>“I think this is analogous to how humans don’t really think about the consequences of their actions, but simply prefer to follow the conventions that we have learned over our lifetimes,” he added. “Another way of saying this is that the stories are surrogate memories for an AI that cannot ‘grow up’ immersed in a society the way people are and must quickly immerse itself in a society by reading about [it].”</p><p>“As the use of AI becomes more prevalent in our society, and as AI becomes more capable, the consequences of their actions become more significant. Giving AIs the ability to read and understand stories may be the most expedient means of enculturing [them] so that they can better integrate themselves into human societies and contribute to our overall wellbeing,” they conclude.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + SliderImageArticle2),
                    SliderImageFileName = SliderImageArticle2,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + HeaderImageArticle2),
                    HeaderImageFileName = HeaderImageArticle2,
                    Category = context.ArticleCategories.Where(c => c.Id == 1).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle2);
                context.SaveChanges();

                var sampleArticle3 = new Article()
                {
                    Title = "Google cleans up search results by ditching sidebar ads",
                    TextHtml = "<p>Google generates a huge amount of revenue through advertising but it's not afraid to try mixing things up a little. Ads in search results have long-been controversial, but the latest change is likely to go down well with many people -- the ads that currently appear in the right-hand sidebar of search results are to be dropped.</p><p>The change means that ads will only be displayed above and below search results. There will be seven Google AdWords ads in total -- four above the search results and three below. The right-hand side of the page will be left free for Google's own Product Listing Ads. Google also confirmed that the change is global and affects all languages.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + SliderImageArticle3),
                    SliderImageFileName = SliderImageArticle3,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + HeaderImageArticle1),
                    HeaderImageFileName = HeaderImageArticle1,
                    Category = context.ArticleCategories.Where(c => c.Id == 1).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle3);
                context.SaveChanges();

                var sampleArticle4 = new Article()
                {
                    Title = "WFP uses innovative iris scan technology to provide food assistance to syrian refugees in Jordan",
                    TextHtml = "<p>Today, a Syrian refugee at King Abdullah Park refugee camp in northern Jordan walked into a supermarket to redeem her monthly food assistance and instead of using her usual prepaid electronic card at the checkout, she looked into an iris scan camera, and paid for her shopping in the twinkling of an eye.</p><p>Today, a Syrian refugee at King Abdullah Park refugee camp in northern Jordan walked into a supermarket to redeem her monthly food assistance and instead of using her usual prepaid electronic card at the checkout, she looked into an iris scan camera, and paid for her shopping in the twinkling of an eye.</p><p>“This a milestone in the evolution of our food assistance programme, which has come so far from the first few months of the Syria crisis, when we distributed food parcels,” said WFP Country Director in Jordan Mageed Yahia.</p><p>With WFP’s iris scan system, we are perfecting the delivery of food assistance, becoming more efficient, enhancing accountability and making grocery shopping easier and more secure for the refugees. The fact that this is happening in Jordan makes it all the more exciting as we hope that it will further contribute to the country’s progress towards being a regional hub for technology.”</p><p>WFP’s innovative system relies on the UN Refugee Agency’s biometric registration data of refugees and works with WFP’s Jordanian partners; IrisGuard, the company that developed the iris scan platform, Jordan Ahli Bank and their counterpart Middle East Payment Systems (MEPS).</p><p>Once the shopper has their iris scanned, the system automatically communicates with UNHCR’s registration database to confirm the identity of the refugee, before automatically continuing to the Jordan Ahli Bank through the Middle East Payment System financial gateway to determine the refugee’s remaining balance. It then confirms the purchase and prints a receipt for the refugee.</p><p>WFP is looking to expand the use of its new iris scan payment system for refugees living in all Syrian refugee camps in Jordan during the coming months. Depending on the success of the system in the camps, WFP may also expand the use of this technology in areas outside of the camps.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + SliderImageArticle4),
                    SliderImageFileName = SliderImageArticle4,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + HeaderImageArticle1),
                    HeaderImageFileName = HeaderImageArticle1,
                    Category = context.ArticleCategories.Where(c => c.Id == 2).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle4);
                context.SaveChanges();

                var sampleArticle5 = new Article()
                {
                    Title = "The legal question at the core of the Apple encryption standoff",
                    TextHtml = "<p>Apple CEO Tim Cook claims that if his company helps the FBI get into a dead terrorist’s iPhone, a precedent would be set that has “implications far beyond” this case. But is that true in the legal sense?</p><p>Technologically speaking, it should be possible for Apple to cooperate with law enforcement—as telecommunications companies have done for 100 years—in this particular case without compromising the tough encryption standards now baked into its phones. So the question then becomes: would this cooperation actually set an unwanted legal precedent for Apple?</p><p>Apple contends the feds are seeking an “unprecedented use” of the All Writs Act, a 1789 law that allows courts to issue orders that pertain to matters not yet covered by a law. But we’re not exactly in uncharted waters. As Kate Knibbs wrote on Gizmodo, the government has leaned several times on the All Writs Act to compel Apple and other communications companies to facilitate investigations. At least one judge has rejected a request on the grounds that the government can’t use the 1789 act to justify pretty much anything that Congress hasn’t yet explicitly allowed or disallowed.</p><p>Now Apple is betting that it can persuade a different judge to see things the same way—and that it's not, as the Justice Department contended on Friday, merely trying to protect its “business model and public brand marketing strategy.”</p><p>So how likely is it that company wins this case? It's very hard to predict, says Orin Kerr, a professor at George Washington University School of Law who specializes in the laws governing computer crime. Kerr wrote in the Washington Post that previous rulings don't match well onto this case, largely because Apple is not a perfect analogue for the traditional telephone companies involved in past surveillance practices. “This case is like a crazy - hard law school exam hypothetical in which a professor gives students an unanswerable problem just to see how they do,” Kerr wrote.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + SliderImageArticle1),
                    SliderImageFileName = SliderImageArticle1,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + HeaderImageArticle2),
                    HeaderImageFileName = HeaderImageArticle2,
                    Category = context.ArticleCategories.Where(c => c.Id == 2).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle5);
                context.SaveChanges();

                var sampleArticle6 = new Article()
                {
                    Title = "Global wind power capacity tops nuclear energy for first time",
                    TextHtml = "<p>The capacity of wind power generation worldwide reached 432.42 gigawatts (GW) at the end of 2015, up 17 percent from a year earlier and surpassing nuclear energy for the first time, according to data released by global industry bodies.</p><p>The generation capacity of wind farms newly built in 2015 was a record 63.01 GW, corresponding to about 60 nuclear reactors, according to the Global Wind Energy Council based in Brussels. The global nuclear power generation capacity was 382.55 GW as of Jan. 1, 2016, the London-based World Nuclear Association said.</p><p>Both wind power and nuclear energy are being touted as alternatives to fossil fuel power as they produce fewer greenhouse gases.</p><p>Wind energy has captured renewed attention as technological innovation has considerably lowered its generation costs while nuclear power continues to suffer a backlash following the 2011 Fukushima meltdowns.</p><p>Wind power is the leading energy source in the transition from fossil fuels to renewables, the wind energy council said as it released the data last week.</p><p>China led all other countries in wind energy generation capacity with 145.10 GW. Beijing is promoting wind power to shift from coal and other fossil fuels to combat air pollution and global warming.</p><p>Coming in second behind China is the United States with 74.47 GW, followed by Germany with 44.95 GW, then India with 25.09 GW and then Spain with 23.03 GW. Japan produced 3.04 GW.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + SliderImageArticle2),
                    SliderImageFileName = SliderImageArticle2,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + HeaderImageArticle1),
                    HeaderImageFileName = HeaderImageArticle1,
                    Category = context.ArticleCategories.Where(c => c.Id == 2).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle6);
                context.SaveChanges();

                var sampleArticle7 = new Article()
                {
                    Title = "Apple says sorry for iPhone error 53 and issues IOS 9.2.1 update to fix it",
                    TextHtml = "<p>Apple has a lot of support at the moment for its stance on encryption and refusing the FBI access to an iPhone's contents, but it's only a couple of weeks since the company was seen in a less favorable light. There was quite a backlash when users found that installing an update to iOS resulted in Error 53 and a bricked iPhone. Apple initially said that Error 53 was caused 'for security reasons' following speculation that it was a bid to stop people from using third party repair shops. iFixit suggested that the problem was a result of a failure of parts to correctly sync, and Apple has been rounding criticized for failing to come up with a fix. Today the company has issued an apology, along with an update that ensures Error 53 won't happen again. But there's more good news ... If you were talked into paying for an out of warranty replacement as a result of Error 53, you could be in line to get your money back.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + SliderImageArticle3),
                    SliderImageFileName = SliderImageArticle3,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + HeaderImageArticle2),
                    HeaderImageFileName = HeaderImageArticle2,
                    Category = context.ArticleCategories.Where(c => c.Id == 3).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle7);
                context.SaveChanges();

                var sampleArticle8 = new Article()
                {
                    Title = "John McAfee offers to decrypt San Bernardino iPhone for the FBI and save America",
                    TextHtml = "<p>Wondering what John McAfee is up to these days? It's not sniffing bath salts nor is he fleeing foreign countries as a person of interest in a murder investigation and faking heart attacks (been there, done all that) ; instead, he's on a mission to save America. How so? By cracking the code on the San Bernardino iPhone that's causing such a ruckus. McAfee didn't just criticize the FBI; instead he offered a potential solution. Let him and his team of hackers break into the iPhone without any help from Apple. \"With all due respect to Tim Cook and Apple, I work with a team of the best hackers on the planet.These hackers attend Defcon in Las Vegas, and they are legends in their local hacking groups, such as HackMiami.They are all prodigies, with talents that defy normal human comprehension,\" McAfee said. Eccentric rant aside, McAfee's offer is simple - give him three weeks and he will, \"free of charge, decrypt the information on the San Bernardino phone\" with his team of hackers. He'll do it using mostly social engineering.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + SliderImageArticle4),
                    SliderImageFileName = SliderImageArticle4,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + HeaderImageArticle1),
                    HeaderImageFileName = HeaderImageArticle1,
                    Category = context.ArticleCategories.Where(c => c.Id == 3).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle8);
                context.SaveChanges();

                var sampleArticle9 = new Article()
                {
                    Title = "LinkedIn is open sourcing their testing frameworks ",
                    TextHtml = "<p>LinkedIn is open sourcing their testing frameworks, and sharing details of their revamped development process after their latest app required a year and over 250 engineers. Their new paradigm? \"Release three times per day, with no more than three hours between when code is committed and when that code is available to members,\" according to a senior engineer on LinkedIn's blog. This requires a three-hour pipeline where everything is automated, from committing code to releasing it into production, along with automated analyses and testing. \"Holding ourselves to this constraint ensures we won't revert to using manual validation to certify our releases.\"</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + SliderImageArticle1),
                    SliderImageFileName = SliderImageArticle1,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + HeaderImageArticle2),
                    HeaderImageFileName = HeaderImageArticle2,
                    Category = context.ArticleCategories.Where(c => c.Id == 4).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle9);
                context.SaveChanges();

                var sampleArticle10 = new Article()
                {
                    Title = "GitHub is undergoing a full-blown overhaul as execs and employees depart",
                    TextHtml = "<p>We've been hearing about a lot of drama going on at $2 billion startup GitHub, the hugely important and popular site used by millions of computer programmers where 10 or more executives have departed in recent months.</p><p>Underlying the drama is the fact that GitHub is trying to grow the company's revenues by landing more big enterprise contracts. And it's doing a good job of that, several sources — even the disgruntled ones — told us.</p><p>That means there's an effort to hire more enterprise salespeople, with all the suit-and-tie salesforce culture that typically includes. (GitHub employs over 80 sales folks according to LinkedIn.) And these sales people want the company to create more products for them to sell.</p><p>Meanwhile, the company's millions of developer users, many of whom use the site for free or for a small monthly fee, also want GitHub to pay more attention to them. A bunch of active and influential users sent a letter in January called \"Dear GitHub\" in which they asked for a bunch of product features, too. At least one person told us that this letter alarmed some of the leadership team.</p><p>Given the growing pains, we've been hearing that the unhappy engineers would like to bail from the company.</p><p>\"Employees live in a culture of fear but the pay is at the 95th percentile and folks just accept the sadly deteriorating culture,\" complained one GitHub employee in a recent review on Glassdoor.</p><p>Some of these folks may be hanging out until GitHub offers some kind of \"liquidity event\" — a way for longtime employees or investors to sell some of their shares — which one person believes could take place soon.</p><p>With plenty of competitors, including Atlassian, GitLab, and even Google, one thing is certain: If GitHub does stumble, there are plenty of companies that want to pick up its slack.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + SliderImageArticle2),
                    SliderImageFileName = SliderImageArticle2,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + HeaderImageArticle1),
                    HeaderImageFileName = HeaderImageArticle1,
                    Category = context.ArticleCategories.Where(c => c.Id == 4).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle10);
                context.SaveChanges();

                var sampleArticle11 = new Article()
                {
                    Title = "NASA aeronautics budget proposes return of X-planes",
                    TextHtml = "<p>If President Obama's recently released federal budget request is approved for the fiscal year beginning Oct. 1, 2016, next year will be the first in a bold 10-year plan by NASA Aeronautics to achieve huge goals in reducing fuel use, emissions, and noise by the way aircraft are designed, and the way they operate in the air and on the ground.</p><p>One exciting piece of this 10-year plan is New Aviation Horizons -- an ambitious undertaking by NASA to design, build and fly a variety of flight demonstration vehicles, or \"X - planes.\" The demos included advancements in lightweight composite materials that are needed to create revolutionary aircraft structures, an advanced fan design to improve propulsion and reduce noise in jet engines, designs to reduce noise from wing flaps and landing gear, and shape-changing wing flaps, and even coating to prevent bug residue buildup on wings.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + SliderImageArticle3),
                    SliderImageFileName = SliderImageArticle3,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + HeaderImageArticle2),
                    HeaderImageFileName = HeaderImageArticle2,
                    Category = context.ArticleCategories.Where(c => c.Id == 5).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle11);
                context.SaveChanges();

                var sampleArticle12 = new Article()
                {
                    Title = "Mice gain weight in cold temperatures due to gut changes ",
                    TextHtml = "<p>Winter is coming, in the northern hemisphere at least. Time, then, to break out the heavy coats, thick gloves and galoshes—but also time, if a study by Mirko Trajkovski of the University of Geneva is correct, for your gut bacteria to remodel your intestines and make them better at absorbing nutrients before the blizzards arrive. Dr Trajkovski’s work, just published in Cell, was on mice. But previous experience suggests that in this area of biology what applies to mice applies to men as well. If that is true in this case too, it will mean an important part of the human body’s thermoregulation is actually controlled by its companion microbes.</p><p>Dr Trajkovski’s research group studies obesity and insulin resistance—the latter being the cause of the form of diabetes many people suffer from in later life. Past studies have shown that obese animals (people included) have different microbial mixtures, known as microbiomes, in their guts from those found in animals of normal weight. Moreover, in mice at least, modifying the mix can induce obesity without a change of diet. One line of inquiry the group is pursuing therefore involves studying murine microbiomes. And one question they asked themselves is what effect ambient temperature might have.</p><p>To find out, they put some mice into one or other of two sets of enclosures for a month. The first set were maintained at 6°C. The second were kept at 22°C (ie, room temperature). The team weighed the animals at regular intervals. They also saved the creatures’ faeces and collected blood samples from which they could determine their subjects’ sensitivity to insulin. Since insulin stimulates the burning by cells of glucose, the more sensitive an animal is to it, the more glucose it will burn, and the more heat it will generate.</p><p>Based on previous findings, Dr Trajkovski expected the mice in the cold enclosures to lose weight as they burned stored reserves to stay warm—and, for the first few days they did. After five to ten days, though, they did something unexpected. In spite of the fact that their rations had not increased, they began to put on weight.</p><p>To try to find out why, the team measured the calorific value left in the faeces, to assess how much nutrition the animals had extracted from their food. They also looked at the insulin-sensitivity data.</p><p>Mice exposed to the cold, they discovered, became 50% more efficient over the course of the study at absorbing nutrients from their food. Those held at room temperature, by contrast, showed no change in their digestive efficiency. The cold-dwelling mice also became 40% more sensitive to insulin, while those in the room-temperature enclosures did not. That suggested the mice in the chiller cabinets were not only extracting more value from their food, they were also becoming better at burning it, and thus generating heat.</p><p>Given its role in obesity, Dr Trajkovski suspected the gut microbiome might be playing a part is these unpredicted results. The team thus reran the experiments and sampled the animals’ gut floras. They also looked at the rodents’ intestinal walls, to see if the anatomy of these had changed in ways that would make it easier for them to absorb food.</p><p>The gut floras of the two groups were radically different. In particular, and intriguingly, the chilled mice lacked a species called Akkermansia muciniphila that is often absent from the guts of obese people—an absence that may be involved in their putting on extra weight.</p><p>The cold-dwelling mice also had different intestinal anatomy. Their villi, the tiny projections from the intestinal wall that absorb food into the body, were more than 50% longer than those of mice living at room temperature.</p><p>Dr Trajkovski and his colleagues then arranged for mice that had been born and raised at room temperature, and in aseptic conditions (and which were thus germ-free), to have bacteria, collected either from cold-dwelling mice or from mice that came from room-temperature enclosures, transplanted into their guts.</p><p>After two weeks, those with transplants from cold-dwelling mice resembled the mice the transplants had come from in insulin sensitivity, cold tolerance and the lengths of their villi. The mice with transplants from “room-temperature” mice, by contrast, resembled those.</p><p>As a final experiment the team added some A. muciniphila to the guts of the mice that had received transplants from cold-dwellers, to see how the bug’s reintroduction would shape them. Remarkably, these mice started losing weight and, when the researchers examined their intestines two weeks later, they found that the villi had shrunk back to the size of those found in room-temperature mice.</p><p>How the bacteria cause all these changes Dr Trajkovski has yet to work out. But the bottom line is clear: in mice—and probably in human beings as well—partial control of the body’s thermostat is in the hands of subcontractors.</p>",
                    SliderImage = imageConverter.ConvertImageToByteArray(pathBase + SliderImageArticle4),
                    SliderImageFileName = SliderImageArticle4,
                    HeaderImage = imageConverter.ConvertImageToByteArray(pathBase + HeaderImageArticle1),
                    HeaderImageFileName = HeaderImageArticle1,
                    Category = context.ArticleCategories.Where(c => c.Id == 5).FirstOrDefault(),
                    Author = context.Users.Where(u => u.UserName == "admin").FirstOrDefault()
                };

                context.Articles.Add(sampleArticle12);
                context.SaveChanges();
            }
        }

        public static void SeedVotes(ApplicationDbContext context)
        {
            var random = new Random();

            if (!context.Votes.Any())
            {
                var articles = context.Articles.ToList();

                foreach (var article in articles)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        var newVote = new Vote()
                        {
                            Article = article,
                            Voter = context.Users.OrderBy(u => Guid.NewGuid()).FirstOrDefault(),
                            VoteType = (VoteType)random.Next(-1, 2)
                        };

                        context.Votes.Add(newVote);
                    }

                    context.SaveChanges();
                }
            }
        }

        public static void SeedComments(ApplicationDbContext context)
        {
            if (!context.Comments.Any())
            {
                var articles = context.Articles.ToList();

                foreach (var article in articles)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var author = context.Users.OrderBy(u => Guid.NewGuid()).FirstOrDefault();
                        var newComment = new Comment()
                        {
                            Article = article,
                            Author = author,
                            CommentText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
                        };

                        context.Comments.Add(newComment);
                        context.SaveChanges();

                        if (i % 2 == 0)
                        {
                            var parentComment = context.Comments.OrderBy(c => Guid.NewGuid()).FirstOrDefault();

                            var commentReply = new Comment()
                            {
                                Article = parentComment.Article,
                                Author = context.Users.OrderBy(u => Guid.NewGuid()).FirstOrDefault(),
                                CommentText = "It is always good to reply to a dummy text comment!"
                            };

                            parentComment.Comments.Add(commentReply);
                            context.Comments.Add(commentReply);
                        }
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
