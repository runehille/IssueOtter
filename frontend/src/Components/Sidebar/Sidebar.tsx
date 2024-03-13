import { FaAngleDoubleRight } from "react-icons/fa";
import { ReactNode } from "react";

type Props = {
  mainContent: () => ReactNode;
  sidebarContent: () => ReactNode;
};

const Sidebar = ({ mainContent, sidebarContent }: Props) => {
  return (
    <div className="container flex">
      <label
        htmlFor="my-drawer-2"
        className="btn btn-sm md:hidden bg-secondary "
      >
        <FaAngleDoubleRight />
      </label>
      <div className="drawer md:drawer-open">
        <input id="my-drawer-2" type="checkbox" className="drawer-toggle" />
        <div className="drawer-content flex items-start justify-start sm:pl-32 sm:pt-12">
          {mainContent()}
        </div>
        <div className="drawer-side rounded-e-xl">
          <label
            htmlFor="my-drawer-2"
            aria-label="close sidebar"
            className="drawer-overlay"
          ></label>
          <ul className="menu p-4 w-72 min-h-full bg-secondary text-base-content">
            {sidebarContent()}
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Sidebar;
