import { createBrowserRouter } from "react-router-dom";
import LoginScreen from "./LoginScreen";
import CreateAccount from "./CreateAccount";
import ForgotPassword from "./ForgotPassword";
import ResetPassword from "./ResetPassword";
import HomePage from "./HomePage";
import Posts from "../components/Posts";
import Conversations from "../components/Conversations";
import UpdateCredentials from "../components/UpdateCredentials";
import ChangePassWord from "../components/ChangePassWord";
import CloseAccount from "../components/CloseAccount";
import CurrentUserProfile from "./CurrentUserProfile";
import UserProfile from "./UserProfile";
import Conversation from "./Conversation";
import ErrorElement from "../components/ErrorElement";


const router = createBrowserRouter([
    {path:'/',element:<LoginScreen/> ,errorElement:<ErrorElement/>},
    {path:'/create-account',element:<CreateAccount/>,errorElement:<ErrorElement/>},
    {path:'/forgot-password',element:<ForgotPassword/>,errorElement:<ErrorElement/>},
    {path:'/reset-password',element:<ResetPassword/>,errorElement:<ErrorElement/>},
    {path:'/current-user',element:<CurrentUserProfile/>,errorElement:<ErrorElement/>},
    {path:'/user',element:<UserProfile/>,errorElement:<ErrorElement/>},
    {path:"/conversation" , element:<Conversation/>,errorElement:<ErrorElement/>},
    {path:'/home-page',element:<HomePage/>,errorElement:<ErrorElement/>,
  
        children:
        [
            {path:'',element:<Posts/>,errorElement:<ErrorElement/>},
            {path:'conversations',element:<Conversations/>,errorElement:<ErrorElement/>},
            {path:'update',element:<UpdateCredentials/>,errorElement:<ErrorElement/>},
            {path:'changepassword',element:<ChangePassWord/>,errorElement:<ErrorElement/>},
            {path:'close',element:<CloseAccount/>,errorElement:<ErrorElement/>},
        ]
    },
   
]);

export default router;