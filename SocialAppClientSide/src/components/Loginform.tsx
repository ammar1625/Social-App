
import React, { useEffect, useRef, useState } from "react";
import { NavLink, useNavigate } from "react-router-dom";
import { useLogIn } from "../hooks/useLogIn";
import { useLoginCredentialsStore } from "../stores/useLoginCredentialsStore";
import { useTwoFaLogin } from "../hooks/usetwoFaLogin";
import { userCurrentUserStore } from "../stores/useCurrentUserStore";
import { useTokensStore } from "../stores/useTokensStore";

function LoginForm()
{
    const navigate = useNavigate();

    const msgRef = useRef<HTMLParagraphElement>(null);
    const emailRef = useRef<HTMLInputElement>(null);
    const passwordRef = useRef<HTMLInputElement>(null);
    const timeOutId = useRef<number|null>(null);
    const loginFormRef  =useRef<HTMLDivElement>(null);
    const twoFaLoginFormRef  =useRef<HTMLDivElement>(null);
    const otpRef = useRef<HTMLInputElement>(null);
    const twoFaMsgRef = useRef<HTMLParagraphElement>(null);
    const [msg,setMsg] = useState("");
    const [code,setCode] = useState(0); 

    const {setCredentials} = useLoginCredentialsStore();
    const {setCurrentUser} =userCurrentUserStore();
    const {setTokens} = useTokensStore();
   
    const loginFields = [emailRef,passwordRef];

    const {data:loginData,error:loginError,isPending:loginPending  , mutate} = useLogIn();
    const {data:twofaLoginData, error:twoFaLoginError,isLoading:twoFaLoginIsLoading} = useTwoFaLogin(code);
    function validateFiedlsNotEmpty(inputs:React.RefObject<HTMLInputElement|null>[])
    {
        let isValid = true;
        inputs.forEach((field)=>{
            if(field.current && !field.current.value)
            {
                field.current.classList.add("invalid-input-field");
                isValid = false;
            }
            else
            {
                if(field.current)
                field.current.classList.remove("invalid-input-field");
            }
        });

        return isValid;
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


    //handle login unexpected error
    useEffect(()=>{
        if(loginError)
        {
            msgRef.current?.classList.remove("invisible");
            setMsg("oops... "+loginError.message);
            if(timeOutId.current)
            {
                clearTimeout(timeOutId.current);
            }
            timeOutId.current = setTimeout(() => {
            msgRef.current?.classList.add("invisible");     
            }, 3500);
        }
    },[loginError]);

    //handle login operation
    useEffect(()=>{
        if(loginData?.isFound===true && loginData.isEmailVerified===true)//seccess login case
        {
            setMsg("log in comfirmation code has been sent to your email box");
            msgRef.current?.classList.add("msg");
            msgRef.current?.classList.remove("invisible");

            emailRef.current?.classList.remove("invalid-input-field");
            passwordRef.current?.classList.remove("invalid-input-field");

            clearFields(loginFields);

            if(timeOutId.current)
            {
                clearTimeout(timeOutId.current);
            }
            timeOutId.current = setTimeout(() => {
                msgRef.current?.classList.remove("msg");
                msgRef.current?.classList.add("invisible");
            }, 4000);

            setTimeout(() => {
                if(loginFormRef.current && twoFaLoginFormRef.current)
                {
                   loginFormRef.current.style.display = "none";
                   twoFaLoginFormRef.current.style.display = "block";
                }
              
            }, 5000);
        }
        else if(loginData?.isFound===true && loginData.isEmailVerified===false) //not verified account case
            {
                setMsg("your account has not yet been verified please click on the verification link in your email box then come back to log in");
                //msgRef.current?.classList.add("msg");
                msgRef.current?.classList.remove("invisible");

                emailRef.current?.classList.remove("invalid-input-field");
                passwordRef.current?.classList.remove("invalid-input-field");

                if(timeOutId.current)
                    {
                        clearTimeout(timeOutId.current);
                    }
                timeOutId.current = setTimeout(() => {
                    //msgRef.current?.classList.remove("msg");
                    msgRef.current?.classList.add("invisible");
                }, 5500);
            }  
        else if(loginData?.isFound==false)//wrong login credentials case
        {
            setMsg("wrong email/password try again");
            emailRef.current?.classList.add("invalid-input-field");
            passwordRef.current?.classList.add("invalid-input-field");
            msgRef.current?.classList.remove("invisible");
            if(timeOutId.current)
                {
                    clearTimeout(timeOutId.current);
                }
            timeOutId.current = setTimeout(() => {
                msgRef.current?.classList.remove("msg");
                msgRef.current?.classList.add("invisible");
            }, 3500);
        }
         
        
    },[loginData]);

    
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

    //handle two fa login error
    useEffect(()=>{
        if(twoFaLoginError)
        {
            twoFaMsgRef.current?.classList.remove("invisible");
            setMsg("oops... "+twoFaLoginError.message);
            if(timeOutId.current)
            {
                clearTimeout(timeOutId.current);
            }
            timeOutId.current = setTimeout(()=>{
            twoFaMsgRef.current?.classList.add("invisible");

            },4000);
        }
    },[twoFaLoginError]);

    //handle 2fa login operation
    useEffect(()=>{
        if(twofaLoginData)
        {
            setCurrentUser(twofaLoginData);
            setTokens(
                {
                    accessToken:twofaLoginData.token,
                    refreshToken:twofaLoginData.refreshToken
                });
            
            //set the login screen visible and hide 2fa login screen
            if(loginFormRef.current && twoFaLoginFormRef.current)
            {
                loginFormRef.current.style.display = "block";
                twoFaLoginFormRef.current.style.display = "none";
            }

            clearFields([otpRef]);
            //take the user to the home page
            navigate("/home-page");
        }
        else
        {
            setMsg("wrong/expired verification code!");
            twoFaMsgRef.current?.classList.remove("invisible");
            if(timeOutId.current)
            clearTimeout(timeOutId.current);

            timeOutId.current = setTimeout(() => {
                twoFaMsgRef.current?.classList.add("invisible");
            }, 4000);

            clearFields([otpRef]);
        }
    },[twofaLoginData])


    return <>
        <div ref={loginFormRef} className="login-form-ctr ">
        <p ref={msgRef} className="error-msg invisible">{msg}</p>

             <form  className="login-form" onSubmit={(e)=>{
                e.preventDefault();

                if(!validateFiedlsNotEmpty(loginFields))
                {
                    msgRef.current?.classList.remove("invisible");
                    setMsg("all fields are required");

                    timeOutId.current = setTimeout(()=>{
                        msgRef.current?.classList.add("invisible");
                    },3500);
                }
                else
                {
                    mutate({email:emailRef.current?.value,passWord:passwordRef.current?.value});
                }
             }}>

            <input ref={emailRef} type="email" className="input-field" placeholder="e-mail" onChange={(e)=>{
                setCredentials({email:e.target.value})
            }}/>
            <input ref={passwordRef} type="password" className="input-field" placeholder="password" onChange={(e)=>{
                setCredentials({passWord:e.target.value})
            }} />
            <button className="btn login-btn">{loginPending?spinner():"Log In"}</button>
            <NavLink to="forgot-password" className="forgot-password-link">forgot password?</NavLink>
            <button className="btn create-account" 
            onClick={()=>navigate("/create-account")}>create account</button>
            </form> 
        </div>
        
        <div ref={twoFaLoginFormRef} className="two-fa-login-ctr ">
        <p ref={twoFaMsgRef} className="error-msg invisible">{msg}</p>

              <form className="two-fa-login-form " onSubmit={(e)=>{
                    e.preventDefault();
                    if(!validateFiedlsNotEmpty([otpRef]))
                    {
                        twoFaMsgRef.current?.classList.remove("invisible");
                        setMsg("you should enter otp code bellow!");
                        twoFaMsgRef.current?.classList.add("invalid-input-field");
                        if(timeOutId.current)
                        {
                            clearTimeout(timeOutId.current);
                        }
                        timeOutId.current = setTimeout(() => {
                            twoFaMsgRef.current?.classList.add("invisible");
                        }, 4000);
                    }
                    else
                    {
                        if(otpRef.current)
                        {
                            setCode(parseInt(otpRef.current.value));
                        }
                    }
              }}>
            
                <div className="title-ctr">
                <p className="forgot-password-title">comfirmation code</p>
                <p className="forgot-password-subtitle">please enter the comfirmation coded that we've sent to your email </p>
                </div>

                <input ref={otpRef} type="number" className="input-field forgot-password-input-field" placeholder="otp code" 
                /* onBlur={(e)=>{
                    if(e.target.value)
                    setCode(parseInt(e.target.value));
                }} *//>

                <div className="buttons-ctr hidden">
                    <button className="cancel-btn" onClick={()=>{
                        if(loginFormRef.current && twoFaLoginFormRef.current)
                        {
                              loginFormRef.current.style.display = "block";
                              twoFaLoginFormRef.current.style.display = "none";
                        }
                       
                    }}>cancel</button>
                    <button className="two-fa-login-btn">{twoFaLoginIsLoading?spinner():"Log In"}</button>
                </div>
            </form>
        </div>
      
    </>
}

export default LoginForm;