import React, { useEffect, useRef, useState } from "react";
import { useValidatePassWord } from "../hooks/useValidatePassWord";
import { userCurrentUserStore } from "../stores/useCurrentUserStore";
import { useIsOverlayVisibleStore } from "../stores/useOverLayVisibleStore";
import { useCloseAccount } from "../hooks/useCloseAccount";
import { useNavigate } from "react-router-dom";


function CloseAccount()
{
    const navigate = useNavigate();
    const {user}  =userCurrentUserStore();

    const timeOutIdRef = useRef<number|null>(null);
    const hasTimeOut = useRef<boolean>(false);

    const closeAccountDialogRef = useRef<HTMLDivElement>(null);
    const msgRef = useRef<HTMLParagraphElement>(null);
    const emailRef = useRef<HTMLInputElement>(null);
    const passWordRef = useRef<HTMLInputElement>(null);

    const [message,setMessage] = useState("");
    const [email,setEmail] = useState("");
    const [passWord,setPassWord] = useState("");
    const {setIsOverlayVisible} = useIsOverlayVisibleStore();


    const {data:validatePassWordData , mutate:mutateValidatePassWord}  =useValidatePassWord();
    const {data:closeAccountData , mutateAsync:mutateCloseAccountAsync} = useCloseAccount();

    function supressError(field:React.RefObject<HTMLInputElement|null>)
    {
        if(field.current)
        {
            field.current.classList.remove("invalid-input-field");
        }
    }
    function validateField(field:React.RefObject<HTMLInputElement|null>,message:string)
    {
        if(field.current)
        {
            if(msgRef.current)
            {
                msgRef.current.classList.remove("invisible");
            }

            if(hasTimeOut.current)
            {
                if(timeOutIdRef.current)
                {
                    clearTimeout(timeOutIdRef.current);
                    hasTimeOut.current = false;
                }
            }

           timeOutIdRef.current =  setTimeout(()=>{
                if(msgRef.current)
                {
                    msgRef.current.classList.add("invisible");
                    hasTimeOut.current = true;
                }
            },3500)

            field.current.classList.add("invalid-input-field");
            setMessage(message);
        }
    }

    function displayElement(element:React.RefObject<HTMLDivElement|null>)
    {
        if(element.current)
        {
            element.current.style.opacity = "100%";
            element.current.classList.remove("invisible");
        }
    }

    function hideElement(element:React.RefObject<HTMLDivElement|null>)
    {
        if(element.current)
        {
            element.current.style.opacity = "0%";
            element.current.classList.add("invisible");
        }
    }

    //handle close account form validation and show close account dialog
    useEffect(()=>{
        if(validatePassWordData)
        {
            
            if(email!= user.email)
            {
                supressError(passWordRef);
                validateField(emailRef,"email does not match your email");
            }
            else if(!validatePassWordData.isValid)
            {
                supressError(emailRef);
                validateField(passWordRef,"password does not match your password");
            }
            else
            {
                supressError(passWordRef);
                //supressError(emailRef);
                setIsOverlayVisible(true);
                displayElement(closeAccountDialogRef);
            }
        }
    },[validatePassWordData]);


    //handle close account after effect and log out
    useEffect(()=>{
        if(closeAccountData)
        {
            if(closeAccountData.isDeleted)
            {
                navigate("/");
            }
        }
    },[closeAccountData]);
    
    return <div className="close-account-ctr">
         
      <div ref={closeAccountDialogRef} className="close-account-dialog invisible" >
            {/* Message */}
            <p className="close-account-dialog-message">
            Do you want to close your account?
            </p>

            {/* Buttons */}
            <div className="close-account-dialog-btns-ctr">
                {/* Cancel Button */}
                <button
                    type="button"
                    className="close-account-dialog-cancel-btn"
                    onClick={()=>{
                        setIsOverlayVisible(false);
                        hideElement(closeAccountDialogRef);
                    }}
                >
                    Cancel
                </button>

                {/* Confirm Button Blue */}
                <button
                    type="button"
                    className="close-account-dialog-comfirm-btn"
                    onClick={async()=>{
                        await mutateCloseAccountAsync({
                            email:user.email,
                            passWord:passWord
                        });

                        setIsOverlayVisible(false);
                        hideElement(closeAccountDialogRef);
                    }}
                >
                    Confirm
                </button>
            </div>
      </div>
                    <div className="login-form-ctr">
                   
                    <p ref={msgRef} className="error-msg invisible">{message}</p>
            
                        <form  className="login-form" onSubmit={(e)=>{
                            e.preventDefault();

                            if(!email||!passWord)
                            {
                                return;
                            }

                            mutateValidatePassWord({
                                userId:user.userId,
                                passWord:passWord
                            })
                        }}>
                        <p className="close-account-title">would you like to close your account?</p>
                        <input ref={emailRef} type="email" className="input-field" placeholder="email..." onChange={(e)=>setEmail(e.target.value.trim())}/>
                        <input ref={passWordRef} type="password" className="input-field" placeholder="password..." onChange={(e)=>setPassWord(e.target.value.trim())}/>
                        <button className="btn login-btn">Close Account</button>
                    
                    {/*  <button className="btn create-account">create account</button> */}
                        </form> 
                    </div>
            </div>
}

export default CloseAccount;