import React, { ReactNode } from 'react';

interface TitleProps
{
    children: ReactNode;
}

export function Title({children}: TitleProps)
{
    return <h1 className="text-3xl pb-5">
        {children}
    </h1>;
}