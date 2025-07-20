// src/components/NavigateSetter.tsx
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useNavigationStore } from "../stores/useNavigationStore";

const NavigationSetter = () => {
  const navigate = useNavigate();
  //const setNavigate = useNavigationStore((state) => state.setNavigate);
  const {setNavigate} = useNavigationStore();

  useEffect(() => {
    setNavigate(navigate);
  }, [navigate, setNavigate]);

  return null; // This component only runs effect
};

export default NavigationSetter;
