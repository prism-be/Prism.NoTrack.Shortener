import { ReactNode } from 'react';

interface BuyButtonProps
{
    children: ReactNode;
    href: string;
    icon: string;
}

export function BuyButton({children, href, icon}: BuyButtonProps)
{
    return (
        <div className="text-center">
            <a className="w-64 mx-auto mt-4 mb-4 py-2 px-4 bg-secondary text-white text-center font-semibold rounded-lg shadow-md border border-secondary hover:border-primary focus:outline-none inline-flex items-center" href={href}>
                <span className="material-icons text-primary pr-2">{icon}</span>
                {children}
            </a>
        </div>
    );
}