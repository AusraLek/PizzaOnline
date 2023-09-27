import { Orders } from "./components/Orders";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/orders',
    element: <Orders />
  }
];

export default AppRoutes;
