import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useForgotPassWord } from "../hooks/useForgotPassword";

function ForgotPassword()
{
    const navigate = useNavigate();
    const emailRef = useRef<HTMLInputElement>(null);
    const msgRef = useRef<HTMLParagraphElement>(null);
    const timeOutId = useRef<number|null>(null);
    const [msg,setMsg] = useState("");
    const {data , isPending , error , mutate} = useForgotPassWord();

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

    useEffect(()=>{
        if(data===true)
        {
            msgRef.current?.classList.remove("invisible");
            msgRef.current?.classList.add("forgot-msg");
            setMsg("an email has been sent to your inbox to reset your password");
            clearFields([emailRef]);

            if(timeOutId.current)
            {
               clearTimeout(timeOutId.current); 
            }
            
            timeOutId.current = setTimeout(() => {
                msgRef.current?.classList.add("invisible");
                msgRef.current?.classList.remove("forgot-msg");
            }, 4000);

           // navigate("/reset-password");
        }
        else if(data===false)
        {
            msgRef.current?.classList.remove("invisible");
            
            setMsg("wrong email! try again");
            clearFields([emailRef]);

            if(timeOutId.current)
            {
               clearTimeout(timeOutId.current); 
            }
            
            timeOutId.current = setTimeout(() => {
                msgRef.current?.classList.add("invisible");
            }, 4000);
        }
    },[data]);

    //handle forgot password request error
    useEffect(()=>{
        if(error)
        {
            setMsg(error.message);
            msgRef.current?.classList.remove("invisible");
            if(timeOutId.current)
            {
                clearTimeout(timeOutId.current);

            }
            timeOutId.current = setTimeout(() => {
                msgRef.current?.classList.add("invisible");
            }, 4000);
        }
    },[error]);

    return  <div className="forgot-password-ctr">

            <p ref={msgRef} className="forgot-error-msg invisible">{msg}</p>
            
            <form className="forgot-password-form" onSubmit={(e)=>{
            e.preventDefault();
            if(emailRef.current)
            {
                if(!emailRef.current.value)
                {
                    return;
                }
            }

            mutate(emailRef.current?.value? emailRef.current?.value:"");

        }}>

            <div className="title-ctr">
            <p className="forgot-password-title">forgot your password?</p>
            <p className="forgot-password-subtitle">please enter your email to send you a link to the page to reset your password</p>
            </div>

            <input ref={emailRef} type="email" className="input-field forgot-password-input-field" placeholder="email" />

            <div className="buttons-ctr">
                <button className="cancel-btn" onClick={()=>{
                    navigate("/");
                }}>cancel</button>
                <button className="comfirm-btn">{isPending?spinner():"comfirm"}</button>
            </div>
        </form>
    </div>

    
}

export default ForgotPassword;