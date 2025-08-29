import { useEffect, useRef, useState } from "react";
import { NavLink } from "react-router-dom";
import { useAddUser } from "../hooks/useAddUser";
import { useIsEmailExists } from "../hooks/useIsEmailExists";
import { useIsUserNameExists } from "../hooks/useIsUserNameExists";

function CreateAccount()
{
    const [gender,setGender] = useState("M");
    const [msg ,setMsg] = useState("");
    const [userEmail,setUserEmail] = useState("");
    const [userName,setUserName] = useState("");
    const timeOutId = useRef<number|null>(null);

    const msgRef = useRef<HTMLParagraphElement>(null);
    const firstNameRef = useRef<HTMLInputElement>(null);
    const lastNameRef = useRef<HTMLInputElement>(null);
    const birthDateRef = useRef<HTMLInputElement>(null);
    const maleRef = useRef<HTMLInputElement>(null);
    const femaleRef = useRef<HTMLInputElement>(null);
    const emailRef = useRef<HTMLInputElement>(null);
    const phoneRef = useRef<HTMLInputElement>(null);
    const userNameRef = useRef<HTMLInputElement>(null);
    const passwordRef = useRef<HTMLInputElement>(null);

    const {data:addedUserData,mutate:addNewUser , isPending ,error:addUserError} = useAddUser();
    const {data:isEmailExists}  =useIsEmailExists(userEmail);
    const {data:isUserNameExists} = useIsUserNameExists(userName);
    
    const now = new Date(); // the the current date
    const maxDateInMelliSeconds =now.setDate(now.getDate()-1);
    const maxDate = new Date(maxDateInMelliSeconds).toISOString().split("T")[0];

    function clearFields()
    {
        const refs = [firstNameRef,lastNameRef,birthDateRef,emailRef,phoneRef,userNameRef,passwordRef];
        refs.forEach((field)=>{
            if(field.current)
            {
                field.current.value ="";
                field.current.classList.remove("invalid-input-field");
            }
        });
    }


    //effect hook to display verification email sent message
    useEffect(()=>{
        if(addedUserData)
        {
            setMsg("verification link email has been sent to your email box,please verify your account");
            msgRef.current?.classList.add("msg");
            msgRef.current?.classList.remove("invisible");

            timeOutId.current = setTimeout(()=>{
                msgRef.current?.classList.add("invisible");
                msgRef.current?.classList.remove("msg");
            },4000);

            clearFields();
        }
    },[addedUserData]);

    //handle add user error
    useEffect(()=>{
        if(addUserError)
        {
            msgRef.current?.classList.remove("invisible");
            setMsg("oops... "+addUserError.message);
            if(timeOutId.current)
                {
                     clearTimeout(timeOutId.current);
                }
           timeOutId.current = setTimeout(() => {
            msgRef.current?.classList.add("invisible");
            }, 3500);
        }
    },[addUserError]);
    

    function validateFieldsNotEmpty():boolean
    {
        let isValid = true;
        const refs = [firstNameRef,lastNameRef,birthDateRef,emailRef,phoneRef,userNameRef,passwordRef];
        refs.forEach((ref)=>{
            if(ref.current && !ref.current.value)
            {
                ref.current.classList.add("invalid-input-field");
                msgRef.current?.classList.remove("invisible");
                isValid = false;
            }
            else
            {
                ref.current?.classList.remove("invalid-input-field");
                msgRef.current?.classList.add("invisible");
             
            }
        });
  
        return isValid;
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

    
    return <div className="create-account-screen">
    <div className="create-account-ctr">

        <p ref={msgRef} className="error-msg invisible">{msg}</p>

            <form className="create-account-form" onSubmit={(e)=>{
                e.preventDefault();

                if(!validateFieldsNotEmpty())
                {
                    setMsg("all fields are required!");
                    msgRef.current?.classList.remove("invisible");
                    msgRef.current?.classList.remove("msg");
                    if(timeOutId.current)
                    {
                         clearTimeout(timeOutId.current);
                    }

                    timeOutId.current =  setTimeout(() => {

                        msgRef.current?.classList.add("invisible");
                    }, 3500);
                    
                }
                else if(isEmailExists)
                {
                    msgRef.current?.classList.remove("invisible");
                    emailRef.current?.classList.add("invalid-input-field")
                    setMsg("email already taken by another user!")

                   timeOutId.current= setTimeout(() => {
                        if(timeOutId.current)
                        {
                            clearTimeout(timeOutId.current);
                        }

                        msgRef.current?.classList.add("invisible");
                    }, 3500);
                }
                else if(isUserNameExists)
                    {
                        msgRef.current?.classList.remove("invisible");
                        userNameRef.current?.classList.add("invalid-input-field")
                        setMsg("user name already taken by another user!")
    
                       timeOutId.current= setTimeout(() => {
                            if(timeOutId.current)
                            {
                                clearTimeout(timeOutId.current);
                            }
    
                            msgRef.current?.classList.add("invisible");
                        }, 3500);
                    }
                else
                {
                    addNewUser(
                        {
                            firstName: firstNameRef.current?.value,
                            lastName:lastNameRef.current?.value,
                            dateOfBirth:birthDateRef.current?.value,
                            email:emailRef.current?.value,
                            phone:phoneRef.current?.value,
                            userName:userNameRef.current?.value,
                            passWord:passwordRef.current?.value,
                            isEmailVerified:false,
                            isActive:true,
                            gender:gender
                        });
                    
                }

                
            }}>
                <div className="create-account-title-ctr">
                    <p className="title">create new account</p>
                    <p className="header-sub-title">it's quick and easy</p>
                </div>

                <div className="name-ctr">
                    <input ref={firstNameRef} type="text" className="input-field name-input-field" placeholder="first name" />
                    <input ref={lastNameRef} type="text" className="input-field name-input-field" placeholder="last name" />
                </div>

                <div className="birthdate-ctr">
                    <p className="sub-title">birthday</p>
                    <input max={maxDate} ref={birthDateRef} type="date" className="input-field birthdate-input-field" />
                </div>

                <div className="gender-field-ctr">
                    <p className="sub-title gender-sub-title">gender</p>

                    <div className="gender-ctr">
                        <p className="sub-title">Male</p>
                        <input ref={maleRef} onChange={(e)=>setGender(e.target.value)} checked = {gender=="M"} type="radio" value="M" name="gender" />
                    </div>
                    <div className="gender-ctr">
                        <p className="sub-title">Female</p>
                        <input ref={femaleRef} onChange={(e)=>setGender(e.target.value)} type="radio" value="F" name="gender" />
                    </div>
                </div>

                <div className="email-ctr">
                    <input ref={emailRef} type="email" className="input-field email-phone-input-field" placeholder="e-mail"
                    onChange={(e)=>setUserEmail(e.target.value)}/>
                </div>

                <div className="phone-ctr">
                <input ref={phoneRef} type="number" className="input-field email-phone-input-field" placeholder="phone"/>

                </div>

                <div className="password-ctr">
                <input ref={userNameRef} type="text" className="input-field name-input-field" placeholder="user name" 
                onChange={(e)=>setUserName(e.target.value)}/>
                <input ref={passwordRef} type="password" className="input-field password-phone-input-field" placeholder="password"/>

                </div>

                <button className="btn create-account-btn"> {isPending?spinner():"Sign Up"} </button>

                <NavLink to="/" className="forgot-password-link mt-3">already have account?</NavLink>
        </form>
    </div>
  
    </div>
 
}

export default CreateAccount;