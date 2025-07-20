import { GrHomeRounded } from "react-icons/gr";
import { SiTheconversation } from "react-icons/si";
import { FaRegUser } from "react-icons/fa6";
import { CgPassword } from "react-icons/cg";
import { MdDeleteOutline } from "react-icons/md";
import { TbLogout2 } from "react-icons/tb";
import { NavLink } from "react-router-dom";
import { useState } from "react";
function HomePageSideBar()
{
    const [isHome,setIsHome] = useState(true);
    return <div className="home-page-side-bar">
        < NavLink className={({isActive})=>isActive && isHome?"nav-link selected":"nav-link"} to="" onClick={()=>setIsHome(true)}><GrHomeRounded size={28}/><span>  Home</span></ NavLink >
        < NavLink className={({isActive})=>isActive?"nav-link selected":"nav-link"} to="conversations" onClick={()=>setIsHome(false)}><SiTheconversation size={28}/><span>  Conversations</span></ NavLink >
        < NavLink className={({isActive})=>isActive?"nav-link selected":"nav-link"} to="update" onClick={()=>setIsHome(false)}><FaRegUser size={28}/><span>  Update Credentials</span></ NavLink >
        < NavLink className={({isActive})=>isActive?"nav-link selected":"nav-link"} to="changepassword" onClick={()=>setIsHome(false)}><CgPassword size={28}/><span>  Change Password</span></ NavLink >
       {/* < NavLink className={({isActive})=>isActive?"nav-link selected":"nav-link"} to="/"><TbLogout2 size={28}/><pre>  Logout</pre></ NavLink >*/}
        < button className="nav-link"><TbLogout2 size={28}/><span>  Logout</span></ button >
        < NavLink className={({isActive})=>isActive?"nav-link selected":"nav-link"} to="close" onClick={()=>setIsHome(false)}><MdDeleteOutline size={28}/><span>  Close Account</span></NavLink>
    </div>
}

export default HomePageSideBar;