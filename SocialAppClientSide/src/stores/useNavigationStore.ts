import { create } from "zustand";

type NavigationStore = {
  navigate: ((path: string) => void) | null;
  setNavigate: (fn: (path: string) => void) => void;
};

export const useNavigationStore = create<NavigationStore>((set) => ({
  navigate: null,
  setNavigate: (fn) => set({ navigate: fn }),
}));