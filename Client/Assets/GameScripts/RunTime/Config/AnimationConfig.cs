namespace GameScript.RunTime.Config
{
    public static class AnimationConfig
    {
        public const string ATTACK1 = "attack1";
        public const string ATTACK2 = "attack2";
        public const string ATTACK3 = "attack3";
        public const string ATTACK4 = "attack4";
        public const string ATTACK5 = "attack5";
        public const string ATTACK6 = "attack6";
        public const string ATTACK7 = "attack7";
        public const string ATTACK8 = "attack8";
        public const string ATTACK9 = "attack9";
        public const string DEFEND = "defend";
        public const string DIE = "die";
        public const string HIT1 = "hit1";
        public const string HIT2 = "hit2";
        public const string HIT_CRIT = "hitCrit";
        public const string IDLE_CITY = "idleCity";
        public const string IDLE_RIDE = "idleRide";
        public const string IDLE_WAR = "idleWar";
        public const string MAGIC = "magic";
        public const string RUN = "run";
        public const string RUN_BACK = "runBack";
        public const string RUN_WAR = "runWar";
        public const string SHOW = "show";
        public const string SHOW2 = "show2";
        public const string WALK = "walk";
        public const string DANCE = "dance";

        public const string WEAPON_ROLE_CREATE1 = "weapon_rolecreate1";
        public const string WEAPON_ROLE_CREATE2 = "weapon_rolecreate2";
        public const string WEAPON_ROLE_CREATE3 = "weapon_rolecreate3";
        public const string WEAPON_ROLE_CREATE4 = "weapon_rolecreate4";
        public const string WEAPON_ROLE_CREATE5 = "weapon_rolecreate5";

        public static readonly string[] Clips = 
        {
            ATTACK1, ATTACK2, ATTACK3, ATTACK4, ATTACK5, ATTACK6, ATTACK7, ATTACK8, ATTACK9, 
            DEFEND, DIE, HIT1, HIT2,HIT_CRIT, IDLE_CITY, IDLE_RIDE, IDLE_WAR, MAGIC, RUN, RUN_BACK, RUN_WAR, SHOW, SHOW2, WALK, 
            DANCE,WEAPON_ROLE_CREATE1,WEAPON_ROLE_CREATE2,WEAPON_ROLE_CREATE3,WEAPON_ROLE_CREATE4,WEAPON_ROLE_CREATE5
        };
    }
}