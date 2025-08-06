import { useEffect, useRef, useState } from "react";
import { useValidatePassWord } from "../hooks/useValidatePassWord";
import { userCurrentUserStore } from "../stores/useCurrentUserStore";

function ChangePassWord()
{
    const {user} = userCurrentUserStore();
    const {data:validatePassWordData , mutateAsync:mutateValidatePassWordAsync,isPending} = useValidatePassWord();

    const [currentPassWord , setCurrentPassWord] = useState("");
    const [newPassWord,setNewPassWord] = useState("");
    const [comfirmedPassWord,setComfirmedPassWord] = useState("");
    const [message,setMessage] = useState("");

    const messageRef = useRef<HTMLParagraphElement>(null);
    const currentPassWordRef = useRef<HTMLInputElement>(null);
    const newPassWordRef = useRef<HTMLInputElement>(null);
    const ComfirmPassWordRef = useRef<HTMLInputElement>(null);

    const timeOutIdref = useRef<number|null>(null);
    const hasTimeOutRef  = useRef<boolean>(false);

    function verifyCurrentPassWord()
    {
       
            if(currentPassWordRef.current)
            {
                currentPassWordRef.current.classList.add("invalid-input-field");
            }

            setMessage("you must enter your current password correctly");

            if(messageRef.current)
            {
                messageRef.current.classList.remove("invisible");
            } 

            if(hasTimeOutRef.current)
            {
                if(timeOutIdref.current)
                {
                    clearTimeout(timeOutIdref.current);
                    hasTimeOutRef.current  =false;
                }
            }

            timeOutIdref.current = setTimeout(() => {
                if(messageRef.current)
                {
                    messageRef.current.classList.add("invisible");
                }
                hasTimeOutRef.current = true;
            }, 3500);

        
    }

    function comfirmPassWord()
    {
        if(messageRef.current)
        {
            messageRef.current.classList.remove("invisible");
        }
        setMessage("password comfirmation does not match");

        if(ComfirmPassWordRef.current)
        {
            ComfirmPassWordRef.current.classList.add("invalid-input-field");
        }

        if(hasTimeOutRef.current)
        {
            if(timeOutIdref.current)
            clearTimeout(timeOutIdref.current);
            hasTimeOutRef.current = false;
        }

        timeOutIdref.current = setTimeout(() => {
            if(messageRef.current)
            {
                messageRef.current.classList.add("invisible");
            }
            hasTimeOutRef.current = true;
        }, 3500);
    }

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

    function clearFields()
    {
        const inputs = [
            currentPassWordRef,newPassWordRef,ComfirmPassWordRef
        ]
        inputs.forEach((field)=>{
            if(field.current)
            {
                field.current.value= "";
                field.current.classList.remove("invalid-input-field");
            }
        });
    }

    useEffect(()=>{
        if(validatePassWordData)
        {
            if(!validatePassWordData?.isValid) //
                {
                    verifyCurrentPassWord();
                }
            else if(newPassWord !== comfirmedPassWord)//
            {
                if(currentPassWordRef.current)
                {
                    currentPassWordRef.current.classList.remove("invalid-input-field");
                }
                    comfirmPassWord();
            }
        }
        
    },[validatePassWordData]);

    return <div className="change-password-ctr">
            <div className="login-form-ctr">
            <p ref={messageRef} className="error-msg invisible">{message}</p>
    
                 <form  className="login-form"
                    onSubmit={async(e)=>{
                        e.preventDefault();

                        if(!currentPassWord || !newPassWord || !comfirmedPassWord)
                        {
                           
                            return;
                        }
                        
                        await mutateValidatePassWordAsync({
                            userId:user.userId,
                            passWord:currentPassWord
                        });

                        //clearFields();
                    }}
                 >
                 <p className="change-password-title">Change your password</p>
                <input ref={currentPassWordRef} type="password" className="input-field" placeholder="your current password..." 
                    onChange={(e)=>{
                        setCurrentPassWord(e.target.value);
                    }}
                    />
                <input ref={newPassWordRef} type="password" className="input-field" placeholder="your new password" 
                    onChange={(e)=>{
                        setNewPassWord(e.target.value);
                    }}
                />
                <input ref={ComfirmPassWordRef} type="password" className="input-field" placeholder="comfirm your password" 
                    onChange={(e)=>{
                        setComfirmedPassWord(e.target.value);
                    }}
                />
                <button className="btn login-btn">{isPending?spinner():"apply changes"}</button>
               
               {/*  <button className="btn create-account">create account</button> */}
                </form> 
            </div>
    </div> 
}

export default ChangePassWord;