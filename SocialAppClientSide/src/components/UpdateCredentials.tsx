import React, { HTMLInputTypeAttribute, useRef, useState } from "react";


function UpdateCredentials()
{
    const timeOutIdIdRef = useRef<number|null>(null);

    const firstNameRef = useRef<HTMLInputElement|null>(null);
    const lastNameRef = useRef<HTMLInputElement|null>(null);
    const emailRef = useRef<HTMLInputElement|null>(null);
    const phoneRef = useRef<HTMLInputElement|null>(null);
    const userNameRef = useRef<HTMLInputElement|null>(null);
    const birthDateRef = useRef<HTMLInputElement|null>(null);
    const messageRef = useRef<HTMLParagraphElement|null>(null);

    const [msg ,setMessage] = useState("");
    const [gender , setGender] = useState("");

    function clearFields(inputs:React.RefObject<HTMLInputElement|null>[])
    {
        inputs.forEach((field)=>{
            if(field.current)
            {
                field.current.value= "";
                field.current.classList.remove("invalid-input-field");
            }
        });
    }

    function validateFieldsNotEmpty(inputs:React.RefObject<HTMLInputElement|null>[]):boolean
    {
        let isValid = true;
        inputs.forEach((field)=>{
            if(field.current)
            {
                if(!field.current.value)
                {
                    field.current.classList.add("invalid-input-field");
                    messageRef.current?.classList.remove("invisible");
                    setMessage("all field are required");

                    if(timeOutIdIdRef.current)
                    {
                         clearTimeout(timeOutIdIdRef.current);
                    }

                    timeOutIdIdRef.current = setTimeout(() => {
                        messageRef.current?.classList.add("invisible");   
                    }, 3000);

                    isValid = false
                }
                else
                {
                    field.current.classList.remove("invalid-input-field");
                }
            }
        });

        return isValid;
    }

    //if all fiedls are empty then show fields required error
    //if user name exists then show user name taken error
    //if email exists then show email taken error
    //otherwise submit the form and show operation done message
    return <div className="update-account-screen">
        <div className="create-account-ctr">
    
            <p ref={messageRef} className="error-msg invisible">something went wrong</p>
    
                <form className="create-account-form">
                    <div className="create-account-title-ctr">
                        <p className="title">Edit Credentials</p>
                        <p className="header-sub-title">it's quick and easy</p>
                    </div>
    
                    <div className="name-ctr">
                        <input ref={firstNameRef} type="text" className="input-field name-input-field" placeholder="first name" />
                        <input ref={lastNameRef} type="text" className="input-field name-input-field" placeholder="last name" />
                    </div>
    
                    <div className="birthdate-ctr">
                        <p className="sub-title">birthday</p>
                        <input ref={birthDateRef} type="date" className="input-field birthdate-input-field" />
                    </div>
    
                    <div className="gender-field-ctr">
                        <p className="sub-title">gender</p>
    
                        <div className="gender-ctr">
                            <p className="sub-title">Male</p>
                            <input  type="radio" value="M" name="gender" />
                        </div>
                        <div className="gender-ctr">
                            <p className="sub-title">Female</p>
                            <input type="radio" value="F" name="gender" />
                        </div>
                    </div>
    
                    <div className="email-ctr">
                        <input ref={emailRef} type="email" className="input-field email-phone-input-field" placeholder="e-mail"/>
                    </div>
    
                    <div className="phone-ctr">
                    <input ref={phoneRef} type="number" className="input-field email-phone-input-field" placeholder="phone"/>
    
                    </div>
    
                    <div className="password-ctr">
                    <input ref={userNameRef} type="text" className="input-field name-input-field update-username-input-field" placeholder="user name" />
                    {/* <input type="password" className="input-field password-phone-input-field" placeholder="password"/> */}
    
                    </div>
    
                    <button className="btn create-account-btn">submit</button>
    
                    {/* <NavLink to="#" className="forgot-password-link mt-3">already have account?</NavLink> */}
            </form>
        </div>
      
        </div>

}

export default UpdateCredentials;