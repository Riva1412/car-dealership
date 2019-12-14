

namespace Car_project
{
    public class UserDesignModel : model_for_each_one
    {
        //getter
        public static UserDesignModel instance => new UserDesignModel();
        //constructor
        public UserDesignModel()
        {
            name = "luke";
            email = "luke@gmail.com";
            button_id = 1;
            initials = "LU";

        }
    }
    public class feedback_design : feedback_contains
    {
        //getter
        public static feedback_design instance => new feedback_design();
        //constructor
        public feedback_design()
        {
            name = "luke";
            email = "luke@gmail.com";
            message = "hi there";
            initials = "L";
            feedback_time = "0 days ago";

        }
    }
}
