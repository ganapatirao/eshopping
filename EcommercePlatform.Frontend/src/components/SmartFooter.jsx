import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { layoutAPI } from '../services/api';

const SmartFooter = () => {
  const [footerData, setFooterData] = useState(null);

  useEffect(() => {
    fetchFooterData();
  }, []);

  const fetchFooterData = async () => {
    try {
      const response = await layoutAPI.getFooter();
      setFooterData(response.data);
    } catch (error) {
      console.error('Error fetching footer data:', error);
    }
  };

  if (!footerData) {
    return (
      <footer className="bg-gray-800 text-white mt-12">
        <div className="container mx-auto px-4 py-8">
          <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
            {[1, 2, 3, 4].map(i => (
              <div key={i} className="h-32 bg-gray-700 animate-pulse rounded"></div>
            ))}
          </div>
        </div>
      </footer>
    );
  }

  return (
    <footer className="bg-gray-800 text-white mt-12">
      <div className="container mx-auto px-4 py-12 pb-24 md:pb-12">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
          {/* Company Info */}
          <div>
            <h3 className="text-xl font-bold mb-4">{footerData.companyName}</h3>
            <p className="text-gray-400">{footerData.description}</p>
          </div>

          {/* Footer Sections */}
          {footerData.sections
            .filter(section => section.isActive)
            .sort((a, b) => a.displayOrder - b.displayOrder)
            .map(section => (
              <div key={section.id}>
                <h4 className="text-lg font-semibold mb-4">{section.title}</h4>
                <ul className="space-y-2">
                  {section.links
                    .filter(link => link.isActive)
                    .sort((a, b) => a.displayOrder - b.displayOrder)
                    .map(link => (
                      <li key={link.id}>
                        <Link
                          to={link.link || '#'}
                          className="text-gray-400 hover:text-white transition-colors"
                        >
                          {link.label}
                        </Link>
                      </li>
                    ))}
                </ul>
              </div>
            ))}
        </div>

        {/* Social Links */}
        {footerData.socialLinks && footerData.socialLinks.length > 0 && (
          <div className="mt-8 pt-8 border-t border-gray-700">
            <div className="flex space-x-4">
              {footerData.socialLinks.map((link, index) => (
                <a
                  key={index}
                  href={link}
                  target="_blank"
                  rel="noopener noreferrer"
                  className="text-gray-400 hover:text-white transition-colors"
                >
                  Social {index + 1}
                </a>
              ))}
            </div>
          </div>
        )}

        {/* Copyright */}
        {footerData.copyrightText && (
          <div className="mt-8 pt-8 border-t border-gray-700 text-center text-gray-400">
            <p>{footerData.copyrightText}</p>
          </div>
        )}
      </div>
    </footer>
  );
};

export default SmartFooter;
