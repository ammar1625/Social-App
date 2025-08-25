import { GrHomeRounded } from "react-icons/gr";
import { SiTheconversation } from "react-icons/si";
import { FaRegUser } from "react-icons/fa6";
import { CgPassword } from "react-icons/cg";
import { MdDeleteOutline } from "react-icons/md";
import { TbLogout2 } from "react-icons/tb";
import { NavLink } from "react-router-dom";
import {  useState } from "react";
//import { useLogOut } from "../hooks/useLogOut";
//import { userCurrentUserStore } from "../stores/useCurrentUserStore";
import { useIsOverlayVisibleStore } from "../stores/useOverLayVisibleStore";
import { useIsLogoutDialogVisibleStore } from "../stores/useIsLogOutDialogVisibleStore";

function HomePageSideBar()
{
    //const navigate = useNavigate();
    //const {user} = userCurrentUserStore();
    const [isHome,setIsHome] = useState(true);
    //const {data:logOutData,mutate:mutateLogOut }  =useLogOut();
    const {setIsOverlayVisible}  = useIsOverlayVisibleStore();
    const {setIsLogOutDialogVisible} = useIsLogoutDialogVisibleStore();
    

    /* //go back to login screen after successfull log out
    useEffect(()=>{ 
        if(logOutData)
        {
            if(logOutData.isLoggedOut)
            {
                navigate("/");
            }
        }
    },[logOutData]); */
    return <div className="home-page-side-bar">


        < NavLink className={({isActive})=>isActive && isHome?"nav-link selected":"nav-link"} to="" onClick={()=>setIsHome(true)}><GrHomeRounded size={28}/><span>  Home</span></ NavLink >
        < NavLink className={({isActive})=>isActive?"nav-link selected":"nav-link"} to="conversations" onClick={()=>setIsHome(false)}><SiTheconversation size={28}/><span>  Conversations</span></ NavLink >
        < NavLink className={({isActive})=>isActive?"nav-link selected":"nav-link"} to="update" onClick={()=>setIsHome(false)}><FaRegUser size={28}/><span>  Update Credentials</span></ NavLink >
        < NavLink className={({isActive})=>isActive?"nav-link selected":"nav-link"} to="changepassword" onClick={()=>setIsHome(false)}><CgPassword size={28}/><span>  Change Password</span></ NavLink >
       {/* < NavLink className={({isActive})=>isActive?"nav-link selected":"nav-link"} to="/"><TbLogout2 size={28}/><pre>  Logout</pre></ NavLink >*/}
        < button className="nav-link" onClick={()=>{
           // mutateLogOut(user.userId);
           setIsOverlayVisible(true);
           setIsLogOutDialogVisible(true);
        
           
        }}><TbLogout2 size={28}/><span>  Logout</span></ button >
        < NavLink className={({isActive})=>isActive?"nav-link selected":"nav-link"} to="close" onClick={()=>setIsHome(false)}><MdDeleteOutline size={28}/><span>  Close Account</span></NavLink>
    </div>
}

export default HomePageSideBar;