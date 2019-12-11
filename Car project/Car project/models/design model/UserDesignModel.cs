namespace Car_project
{
   public class UserDesignModel:model_for_each_one
    {
        //getter
        public static UserDesignModel instance=>new UserDesignModel();
        //constructor
        public UserDesignModel()
        {
            name = "luke";
            email = "luke@gmail.com";
            button_id = 1;
            initials = "LU";

        }
    }
}
