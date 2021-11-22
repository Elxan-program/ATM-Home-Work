using ATM_Home_Work.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Home_Work.Repository
{
	class FRepos
	{
		public List<Card> GetAll()
		{
			return new List<Card>
{
new Card
{
CardCode="2222333344445555",
UserName="Username",
Balance="566233.221"
},
new Card
{
CardCode="7777888899990000",
UserName="Username1",
Balance="40.5"
}
};
		}
	}
}
