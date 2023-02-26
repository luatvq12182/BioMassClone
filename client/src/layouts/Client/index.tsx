import { Outlet, NavLink } from "react-router-dom";

type Props = {};

const ClientLayout = ({}: Props) => {
    return (
        <div>
            <header>
                <nav>
                    <ul>
                        <li>
                            <NavLink to='/'>Home</NavLink>
                        </li>
                        <li>
                            <NavLink to='/products'>Products</NavLink>
                        </li>
                        <li>
                            <NavLink to='/articles'>News</NavLink>
                        </li>
                        <li>
                            <NavLink to='/contact'>Contact</NavLink>
                        </li>
                    </ul>
                </nav>
            </header>

            <section id='slider'>Slider</section>

            <main>
                <Outlet />
            </main>

            <footer>Footer</footer>
        </div>
    );
};

export default ClientLayout;
