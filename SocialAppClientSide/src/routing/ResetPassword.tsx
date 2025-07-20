import { useEffect, useRef, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { useResetPassWord } from "../hooks/useResetPassWord";

function ResetPassword()
{
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();
    const Token = searchParams.get("token");
    const userEmail = searchParams.get("useremail");

    const msgRef = useRef<HTMLParagraphElement>(null);
    const passWordRef = useRef<HTMLInputElement>(null);
    const timeOutId = useRef<number|null>(null);
    const [msg,setMsg] = useState("");
    
    const {data,isPending,error,mutate} = useResetPassWord();

   

    function spinner()
    {
        return <svg
        className="animate-spin text-white w-[1.3em] h-[1.3em]"
        xmlns="http://www.w3.org/2000/svg"
        fill="none"
        viewBox="0 0 24 24"
      >
        <circle
          className="opacity-25"
          cx="12"
          cy="12"
          r="10"
          stroke="currentColor"
          strokeWidth="4"
        />
        <path
          className="opacity-75"
          fill="currentColor"
          d="M12 2a10 10 0 0110 10h-4a6 6 0 00-6-6V2z"
        />
      </svg>
    }

       function clearFields(fields:React.RefObject<HTMLInputElement|null>[])
        {
            fields.forEach((field)=>{
                if(field.current)
                {
                    field.current.value = "";
                    field.current.classList.remove("invalid-input-field");
                }
            });
        }
    //handle request error
    useEffect(()=>{
        if(error)
        {
            msgRef.current?.classList.remove("invisible");
            setMsg(error.message);
            if(timeOutId.current)
            {
                clearTimeout(timeOutId.current);
            }

            timeOutId.current = setTimeout(() => {
                msgRef.current?.classList.add("invisible");
            }, 4000);
            clearFields([passWordRef]);

        }
    },[error]);


    //handle request
    useEffect(()=>{
        if(data===true)
        {
            msgRef.current?.classList.add("msg");
            msgRef.current?.classList.remove("invisible");
            setMsg("your password has been reset successfully you wil be redirected to login screen");

            if(timeOutId.current)
            {
                clearTimeout(timeOutId.current);
            }
            timeOutId.current = setTimeout(() => {
                msgRef.current?.classList.remove("msg");
                msgRef.current?.classList.add("invisible");
                navigate("/");
            }, 5000);
            clearFields([passWordRef]);

        }
        else if(data===false)
        {
            msgRef.current?.classList.remove("invisible");
            setMsg("something went wrong!");

            if(timeOutId.current)
                {
                    clearTimeout(timeOutId.current);
                }
                timeOutId.current = setTimeout(() => {
                    msgRef.current?.classList.add("invisible");
                }, 5000);
                clearFields([passWordRef]);
        }
    },[data]);

    return <div className="reset-password-ctr">
        <p ref={msgRef} className="error-msg reset-password-error-msg invisible">{msg}</p>

             <form className="forgot-password-form" onSubmit={(e)=>{
                e.preventDefault();

                if(passWordRef.current)
                {
                    if(!passWordRef.current.value)
                    {
                        return;
                    }
                }

                mutate({email:userEmail,token:Token,newPassword:passWordRef.current?.value});
             }}>

            <div className="title-ctr">
            <p className="forgot-password-title">reset your password</p>
            <p className="forgot-password-subtitle">please enter your new password </p>
            </div>

            <input ref={passWordRef} type="password" className="input-field forgot-password-input-field" placeholder="password" />

            <div className="buttons-ctr">
                <button className="cancel-btn" onClick={()=>{
                    navigate("/forgot-password");
                }}>cancel</button>
                <button className="comfirm-btn">{isPending?spinner():"reset"}</button>
            </div>
             </form>
        
       
    </div>
}

export default ResetPassword;