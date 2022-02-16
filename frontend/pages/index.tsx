import type { NextPage } from 'next';
import { About } from '../components/About';
import { Shorten } from '../components/Shorten';

const Home: NextPage = () =>
{
    return (
        <>
            <Shorten/>
            <About/>
        </>
    );
};

export default Home;
