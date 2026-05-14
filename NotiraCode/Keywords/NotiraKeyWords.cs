using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Keywords
{
 






internal class NotiraKeyWords
    {
        [CustomEnum("Choice")]
        // 放在原版卡牌描述的位置，这里是卡牌描述的前面
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword Choice;

        [CustomEnum("rebellious")]
        // 放在原版卡牌描述的位置，这里是卡牌描述的前面
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword rebellious;

        [CustomEnum("tucao1")]
        // 放在原版卡牌描述的位置，这里是卡牌描述的前面
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword tucao1;


        [CustomEnum("tucao2")]
        // 放在原版卡牌描述的位置，这里是卡牌描述的前面
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword tucao2;


        [CustomEnum("Gungun")]
        // 放在原版卡牌描述的位置，这里是卡牌描述的前面
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword Gungun;


        [CustomEnum("CL")]
        // 放在原版卡牌描述的位置，这里是卡牌描述的前面
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword CL;

        [CustomEnum("Gift")]
        // 放在原版卡牌描述的位置，这里是卡牌描述的前面
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword Gift;
            


    }
}
