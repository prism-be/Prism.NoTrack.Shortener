import React, { ReactNode } from 'react';

interface SubTitleProps
{
    children: ReactNode;
}

export function SubTitle({children}: SubTitleProps)
{
    return <h2 className="text-2xl pt-3 pb-2">
        {children}
    </h2>;
}