import { createContext, useState, useEffect, ReactNode } from "react";
import { setAuthorizationHeader } from "@/services/httpClient";

type Context = {
    user: string | null;
    signIn: (newUser: string, callback: VoidFunction) => void;
    signOut: (callback: VoidFunction) => void;
};

type Props = {
    children: ReactNode;
};

const AuthContext = createContext<Context>({
    user: null,
    signIn: () => {},
    signOut: () => {},
});

const AuthProvider = ({ children }: Props) => {
    const [user, setUser] = useState<string | null>(null);
    const [isDoneChecking, setIsDoneChecking] = useState(false);

    const signIn = (newUser: string, callback: VoidFunction) => {
        setUser(newUser);
        callback();
    };

    const signOut = (callback: VoidFunction) => {
        setUser(null);
        callback();
    };

    const value = {
        user,
        signIn,
        signOut,
    };

    useEffect(() => {
        const accessToken = localStorage.getItem("accessToken");
        let user: any = localStorage.getItem("user");

        if (accessToken && user) {
            user = JSON.parse(user);

            setUser(user.username);
            setAuthorizationHeader(accessToken);
        }

        setIsDoneChecking(true);
    }, []);

    if (!isDoneChecking) return <div>'Loading...'</div>;

    return (
        <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
    );
};

export { AuthContext, AuthProvider };
