import type { NextPage } from 'next';
import Head from 'next/head';
import Image from 'next/image';
import styles from '../styles/Home.module.css';
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
