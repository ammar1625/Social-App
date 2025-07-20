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


const router = createBrowserRouter([
    {path:'/',element:<LoginScreen/>},
    {path:'/create-account',element:<CreateAccount/>},
    {path:'/forgot-password',element:<ForgotPassword/>},
    {path:'/reset-password',element:<ResetPassword/>},
    {path:'/current-user',element:<CurrentUserProfile/>},
    {path:'/user',element:<UserProfile/>},
    {path:"/conversation" , element:<Conversation/>},
    {path:'/home-page',element:<HomePage/>,
  
        children:
        [
            {path:'',element:<Posts/>},
            {path:'conversations',element:<Conversations/>},
            {path:'update',element:<UpdateCredentials/>},
            {path:'changepassword',element:<ChangePassWord/>},
            {path:'close',element:<CloseAccount/>},
        ]
    },
   
]);

export default router;