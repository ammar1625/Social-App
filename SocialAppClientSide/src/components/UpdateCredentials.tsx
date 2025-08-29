import React, { useEffect, useRef, useState } from "react";
import { userCurrentUserStore } from "../stores/useCurrentUserStore";
import { useIsEmailExists } from "../hooks/useIsEmailExists";
import { useIsUserNameExists } from "../hooks/useIsUserNameExists";
import { useUpdateCredentials } from "../hooks/useUpdateCredentials";


function UpdateCredentials()
{
    const {user ,setCurrentUser} =userCurrentUserStore();
    const timeOutIdIdRef = useRef<number|null>(null);
    const hasTimeOut = useRef<boolean>(false);

    const firstNameRef = useRef<HTMLInputElement|null>(null);
    const lastNameRef = useRef<HTMLInputElement|null>(null);
    const emailRef = useRef<HTMLInputElement|null>(null);
    const phoneRef = useRef<HTMLInputElement|null>(null);
    const userNameRef = useRef<HTMLInputElement|null>(null);
    const birthDateRef = useRef<HTMLInputElement|null>(null);
    const messageRef = useRef<HTMLParagraphElement|null>(null);

    const [firstName ,setFirstName] = useState("");
    const [lastName ,setLastName] = useState("");
    const [dateOfBirth ,setDateOfBirth] = useState("");
    const [phone ,setPhone] = useState("");
    const [email ,setEmail] = useState("");
    const [userName,setUserName] = useState("");
    const [gender , setGender] = useState(user.gender);

    console.log(userNameRef.current?.value);
    const {data:isEmailExistsData} = useIsEmailExists(email.trim() ,user.userId);
    const {data:isUserNameExistsData} = useIsUserNameExists(userName.trim(),user.userId);
    const {data:updatedUserData , mutateAsync:mutateUserNewDataAsync , isPending} =useUpdateCredentials();

    const [msg ,setMessage] = useState("");

    const now = new Date(); // the the current date
    const maxDateInMelliSeconds =now.setDate(now.getDate()-1);
    const maxDate = new Date(maxDateInMelliSeconds).toISOString().split("T")[0];
   
    //after effect hook to display the updated user data immediately
    useEffect(()=>{
        if(updatedUserData)
        {
            setCurrentUser({...user,
            firstName:updatedUserData.firstName,
            lastName:updatedUserData.lastName,
            userName:updatedUserData.userName,
            email:updatedUserData.email,
            phone:updatedUserData.email,
            dateOfBirth:updatedUserData.dateOfBirth,
            gender:updatedUserData.gender
        });


        if(timeOutIdIdRef.current && hasTimeOut.current)
            {
                clearTimeout(timeOutIdIdRef.current);
                hasTimeOut.current = false;
            }
    
            if(messageRef.current)
            {
                    messageRef.current.classList.remove("invisible");
                    messageRef.current.classList.add("msg");
                    setMessage("your infos has been updated successfully");
            }
    
            timeOutIdIdRef.current = setTimeout(() => {
                if(messageRef.current)
                {
                    messageRef.current.classList.remove("msg");
                    messageRef.current.classList.add("invisible");
                    hasTimeOut.current = true;
                }
            }, 3500);
        }

       

        clearFields();
    },[updatedUserData]);

    function clearFields()
    {
        const inputs = [
            firstNameRef,lastNameRef,emailRef,phoneRef,userNameRef,birthDateRef
        ]
        inputs.forEach((field)=>{
            if(field.current)
            {
                field.current.value= "";
                field.current.classList.remove("invalid-input-field");
            }
        });
    }
/* 
    function distinguishErrors
    {
        const inputs = [
            firstNameRef,lastNameRef,emailRef,phoneRef,userNameRef,birthDateRef
        ]
        inputs.forEach((field)=>{
            if(field.current)
            {
                field.current.value= "";
                field.current.classList.remove("invalid-input-field");
            }
        });
    } */

    function handleUniqueField(field:React.RefObject<HTMLInputElement|null> , fieldType:string)
    {
        if(field.current)
            {
                field.current.classList.add("invalid-input-field");
                if(timeOutIdIdRef.current && hasTimeOut.current)
                {
                    clearTimeout(timeOutIdIdRef.current);
                    hasTimeOut.current = false;
                }
                if(messageRef.current)
                {
                    messageRef.current.classList.remove("invisible");
                    setMessage(`${fieldType} already exists`);
                   
                }
                timeOutIdIdRef.current = setTimeout(() => {
                    if(messageRef.current)
                        messageRef.current.classList.add("invisible");
                    hasTimeOut.current = true;
                }, 3500);
            }
    }

    function validateFields():boolean
    {
        const isValid = [
            firstName,
            lastName,
            email,
            phone,
            userName,
            dateOfBirth,
          ].some(f=>f.trim()!==""); // returns true if at least one field is not empty
      
               if(!isValid)
               {
                if(messageRef.current)
                {
                    messageRef.current.classList.remove("invisible");
                }
                if(hasTimeOut.current && timeOutIdIdRef.current)
                {
                    clearTimeout(timeOutIdIdRef.current);
                    hasTimeOut.current = false;
                }

                timeOutIdIdRef.current = setTimeout(() => {
                    if(messageRef.current)
                    {
                        messageRef.current.classList.add("invisible");
                        hasTimeOut.current = true;
                    }
                }, 3500);
        
            }

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
    return <div className="update-account-screen">
        <div className="create-account-ctr">
    
            <p ref={messageRef} className="error-msg invisible">{msg}</p>
    
                <form className="create-account-form" 
                onSubmit={async(e)=>{
                    e.preventDefault();

                    if(!validateFields())
                    {
                        setMessage("you must fill at least one field!");
                    }
                    else if(isEmailExistsData)
                    {
                       handleUniqueField(emailRef,"email");
                    }
                    else if(isUserNameExistsData)
                    {
                      
                        handleUniqueField(userNameRef,"user name");
                    }
                    else
                    {
                       await mutateUserNewDataAsync({
                            userId:user.userId,
                            firstName:firstName?firstName:user.firstName,
                            lastName:lastName?lastName:user.lastName,
                            userName:userName?userName:user.userName,
                            email:email?email:user.email,
                            phone:phone?phone:user.phone,
                            dateOfBirth:dateOfBirth?dateOfBirth:user.dateOfBirth,
                            gender:gender
                        });

                        clearFields();
                    }
                }}>
                    <div className="create-account-title-ctr">
                        <p className="title">Edit Credentials</p>
                        <p className="header-sub-title">it's quick and easy</p>
                    </div>
    
                    <div className="name-ctr">
                        <input  onChange={(e)=>setFirstName(e.target.value.trim())} ref={firstNameRef} type="text" className="input-field name-input-field" placeholder="first name" />
                        <input  onChange={(e)=>setLastName(e.target.value.trim())} ref={lastNameRef} type="text" className="input-field name-input-field" placeholder="last name" />
                    </div>
    
                    <div className="birthdate-ctr">
                        <p className="sub-title">birthday</p>
                        <input value={user.dateOfBirth} max={maxDate.toString()} onChange={(e)=>setDateOfBirth(e.target.value.trim())} ref={birthDateRef} type="date" className="input-field birthdate-input-field" />
                    </div>
    
                    <div className="gender-field-ctr">
                        <p className="sub-title gender-sub-title">gender</p>
    
                        <div className="gender-ctr">
                            <p className="sub-title">Male</p>
                            <input checked = {gender==="M"}  type="radio" value="M" name="gender" onChange={(e)=>setGender(e.target.value)}/>
                        </div>
                        <div className="gender-ctr">
                            <p className="sub-title">Female</p>
                            <input checked = {gender==="F"} type="radio" value="F" name="gender"  onChange={(e)=>setGender(e.target.value)}/>
                        </div>
                    </div>
    
                    <div className="email-ctr">
                        <input  ref={emailRef} type="email" className="input-field email-phone-input-field" placeholder="e-mail"
                        onChange={(e)=>setEmail(e.target.value.trim())}/>
                    </div>
    
                    <div className="phone-ctr">
                    <input  onChange={(e)=>setPhone(e.target.value.trim())} ref={phoneRef} type="number" className="input-field email-phone-input-field" placeholder="phone"/>
    
                    </div>
    
                    <div className="password-ctr">
                    <input  ref={userNameRef} type="text" className="input-field name-input-field update-username-input-field" placeholder="user name" 
                    onChange={(e)=>setUserName(e.target.value.trim())}/>
                    {/* <input type="password" className="input-field password-phone-input-field" placeholder="password"/> */}
    
                    </div>
    
                    <button className="btn create-account-btn">{isPending?spinner():"submit"}</button>
    
                    {/* <NavLink to="#" className="forgot-password-link mt-3">already have account?</NavLink> */}
            </form>
        </div>
      
        </div>

}

export default UpdateCredentials;