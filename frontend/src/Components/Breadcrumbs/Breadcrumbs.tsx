import { useLocation } from "react-router";

const Breadcrumbs = () => {
  const location = useLocation();

  const paths = location.pathname.split("/").filter((path) => path !== "");
  return (
    <div className="text-sm breadcrumbs">
      <ul>
        {paths.map((path, index) => (
          <li key={index}>{path}</li>
        ))}
      </ul>
    </div>
  );
};

export default Breadcrumbs;
