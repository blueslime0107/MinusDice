using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CardId_1{

    class Ability_First : CardAbility{
        public override bool Condition(CardPack card)
        {
            return BattleManager.init.clashWinner == card.owner && BattleManager.init.curState == BattleManager.BattleState.ClashFin;
        }

        public override void Affect(CardPack card){
            BattleManager.init.clashDamage += 2;
        }
    }
}

class CardId_2{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card)
        {
            return card.ClashWinned() && BattleManager.init.curState == BattleManager.BattleState.ClashFin;
        }

        public override void Affect(CardPack card){
            card.SetClashDamage(BattleManager.init.clashDamage + 2);
        }
    }
}

class CardId_3{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_4{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_5{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_6{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_7{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_8{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_9{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_10{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_11{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_12{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_13{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_14{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_15{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_16{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_17{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}

class CardId_18{
    
    class Ability_First : CardAbility{
        public override bool Condition(CardPack card){
            return true;
        }
        public override void Affect(CardPack card){
            Debug.Log("Play");
        }
    }
}